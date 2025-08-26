// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageProcessors;
using Microsoft.Extensions.Logging;
using V3Api = Defra.Trade.Catch.Certificate.Internal.V3INTERNAL.ApiClient.Api;
using V3ApiModel = Defra.Trade.Catch.Certificate.Internal.V3INTERNAL.ApiClient.Model;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.MessageProcessors;

public class ProcessingStatementMessageProcessor(
    V3Api.IMmoProcessingStatementApi apiClient,
    ILogger<ProcessingStatementMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V3Inbound.ProcessingStatementCreateInbound, StandardMessageHeader, V3Api.IMmoProcessingStatementApi, V3ApiModel.ProcessingStatement>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Processing Statement";

    protected override string IdName => nameof(V3Inbound.ProcessingStatementCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.ProcessingStatementSubmittedMessageLabelPrefix,
        ApplicationConstants.ProcessingStatementVoidedMessageLabelPrefix
    };

    protected override string GetId(V3Inbound.ProcessingStatementCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V3ApiModel.ProcessingStatement model)
    {
        model._Version ??= 3;
        var result = await ApiClient.CreateProcessingStatementWithHttpInfoAsync(ApplicationConstants.ApiVersion3, model);
        return result.StatusCode;
    }
}
