// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageProcessors;
using Microsoft.Extensions.Logging;
using V2Api = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Api;
using V2ApiModel = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Model;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.MessageProcessors;

public class ProcessingStatementMessageProcessor(
    V2Api.IMmoProcessingStatementApi apiClient,
    ILogger<ProcessingStatementMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V2Inbound.ProcessingStatementCreateInbound, StandardMessageHeader, V2Api.IMmoProcessingStatementApi, V2ApiModel.ProcessingStatement>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Processing Statement";

    protected override string IdName => nameof(V2Inbound.ProcessingStatementCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.ProcessingStatementSubmittedMessageLabelPrefix,
        ApplicationConstants.ProcessingStatementVoidedMessageLabelPrefix
    };

    protected override string GetId(V2Inbound.ProcessingStatementCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V2ApiModel.ProcessingStatement model)
    {
        model._Version ??= 2;
        var result = await ApiClient.CreateProcessingStatementWithHttpInfoAsync(ApplicationConstants.ApiVersion2, model);
        return result.StatusCode;
    }
}
