// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.Common.Exceptions;
using Defra.Trade.Common.Functions.Interfaces;
using Defra.Trade.Common.Functions.Models;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageProcessors;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "Low complexity")]
public abstract class BaseApiMessageProcessor<TInbound, THeader, TApi, TApiModel> : IMessageProcessor<TInbound, THeader>
    where THeader : BaseMessageHeader
{
    private readonly ILogger<BaseApiMessageProcessor<TInbound, THeader, TApi, TApiModel>> _logger;
    private readonly IMapper _mapper;

    protected TApi ApiClient { get; }

    protected abstract string EntityType { get; }

    protected abstract string IdName { get; }

    protected abstract IEnumerable<string> LabelPrefixes { get; }

    protected BaseApiMessageProcessor(
        TApi apiClient,
        ILogger<BaseApiMessageProcessor<TInbound, THeader, TApi, TApiModel>> logger,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(apiClient);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(mapper);
        ApiClient = apiClient;
        _logger = logger;
        _mapper = mapper;
    }

    public virtual Task<CustomMessageHeader> BuildCustomMessageHeaderAsync()
    {
        return Task.FromResult(new CustomMessageHeader());
    }

    public virtual Task<string> GetSchemaAsync(THeader messageHeader)
    {
        return Task.FromResult(string.Empty);
    }

    public async Task<StatusResponse<TInbound>> ProcessAsync(TInbound model, THeader messageHeader)
    {
        string documentId = GetId(model);
        _logger.LogInformation("Create {EntityType} for {IdName}: {DocumentId}", EntityType, IdName, documentId);

        var apiModel = _mapper.Map<TApiModel>(model);

        var status = await SendAsync(apiModel);
        if (status is < HttpStatusCode.OK or >= HttpStatusCode.BadRequest)
        {
            throw new MessageProcessorException(
                messageHeader.MessageId ?? string.Empty,
                $"Failed to create {EntityType} with status code {status} for {IdName} = {documentId}");
        }

        _logger.LogInformation("{EntityType} for {IdName} {DocumentNumber} is created", EntityType, IdName, documentId);

        return new() { Response = model };
    }

    public Task<bool> ValidateMessageLabelAsync(THeader messageHeader)
    {
        return Task.FromResult(ValidateMessageLabel(messageHeader));
    }

    protected abstract string GetId(TInbound inbound);

    protected abstract Task<HttpStatusCode> SendAsync(TApiModel model);

    protected virtual bool ValidateMessageLabel(THeader messageHeader)
    {
        return messageHeader.Label is string label && LabelPrefixes.Any(prefix => label.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }
}
