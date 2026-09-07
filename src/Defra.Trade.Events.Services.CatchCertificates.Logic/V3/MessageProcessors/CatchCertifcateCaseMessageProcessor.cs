// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.Common.Functions.Isolated.Models;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageProcessors;
using Microsoft.Extensions.Logging;
using V3Api = Defra.Trade.Catch.Certificate.Internal.V3INTERNAL.ApiClient.Api;
using V3ApiModel = Defra.Trade.Catch.Certificate.Internal.V3INTERNAL.ApiClient.Model;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.MessageProcessors;

public class CatchCertificateCaseMessageProcessor(
    V3Api.IMmoCatchCertificateCaseApi apiClient,
    ILogger<CatchCertificateCaseMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V3Inbound.CatchCertificateCaseCreateInbound, TradeEventMessageHeader, V3Api.IMmoCatchCertificateCaseApi, V3ApiModel.CatchCertificateCase>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Catch Certificate";

    protected override string IdName => nameof(V3Inbound.CatchCertificateCaseCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.CatchCertificateSubmittedMessageLabelPrefix,
        ApplicationConstants.CatchCertificateVoidedMessageLabelPrefix
    };

    protected override string GetId(V3Inbound.CatchCertificateCaseCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V3ApiModel.CatchCertificateCase model)
    {
        model._Version ??= 3;
        var result = await ApiClient.CreateCatchCertificateCaseWithHttpInfoAsync(ApplicationConstants.ApiVersion3, model);
        return result.StatusCode;
    }
}
