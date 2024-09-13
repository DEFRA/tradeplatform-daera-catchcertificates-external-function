// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageProcessors;
using Microsoft.Extensions.Logging;
using V1Api = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Api;
using V1ApiModel = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Model;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.MessageProcessors;

public class ProcessingStatementMessageProcessor(
    V1Api.IMmoProcessingStatementApi apiClient,
    ILogger<ProcessingStatementMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V1Inbound.ProcessingStatementCreateInbound, StandardMessageHeader, V1Api.IMmoProcessingStatementApi, V1ApiModel.ProcessingStatement>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Processing Statement";

    protected override string IdName => nameof(V1Inbound.ProcessingStatementCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.ProcessingStatementSubmittedMessageLabelPrefix,
        ApplicationConstants.ProcessingStatementVoidedMessageLabelPrefix
    };

    protected override string GetId(V1Inbound.ProcessingStatementCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V1ApiModel.ProcessingStatement model)
    {
        var result = await ApiClient.MmoProcessingStatementPostWithHttpInfoAsync(ApplicationConstants.ApiVersion1, model);
        return result.StatusCode;
    }
}
