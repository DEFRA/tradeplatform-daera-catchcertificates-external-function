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

public class CatchCertificateCaseMessageProcessor(
    V2Api.IMmoCatchCertificateCaseApi apiClient,
    ILogger<CatchCertificateCaseMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V2Inbound.CatchCertificateCaseCreateInbound, StandardMessageHeader, V2Api.IMmoCatchCertificateCaseApi, V2ApiModel.CatchCertificateCase>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Catch Certificate";

    protected override string IdName => nameof(V2Inbound.CatchCertificateCaseCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.CatchCertificateSubmittedMessageLabelPrefix,
        ApplicationConstants.CatchCertificateVoidedMessageLabelPrefix
    };

    protected override string GetId(V2Inbound.CatchCertificateCaseCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V2ApiModel.CatchCertificateCase model)
    {
        model._Version ??= 2;
        var result = await ApiClient.CreateCatchCertificateCaseWithHttpInfoAsync(ApplicationConstants.ApiVersion2, model);
        return result.StatusCode;
    }
}
