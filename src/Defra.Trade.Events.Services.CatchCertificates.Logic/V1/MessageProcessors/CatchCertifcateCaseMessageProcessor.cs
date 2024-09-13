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

public class CatchCertificateCaseMessageProcessor(
    V1Api.IMmoCatchCertificateCaseApi apiClient,
    ILogger<CatchCertificateCaseMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V1Inbound.CatchCertificateCaseCreateInbound, StandardMessageHeader, V1Api.IMmoCatchCertificateCaseApi, V1ApiModel.CatchCertificateCase>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Catch Certificate";

    protected override string IdName => nameof(V1Inbound.CatchCertificateCaseCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.CatchCertificateSubmittedMessageLabelPrefix,
        ApplicationConstants.CatchCertificateVoidedMessageLabelPrefix
    };

    protected override string GetId(V1Inbound.CatchCertificateCaseCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V1ApiModel.CatchCertificateCase model)
    {
        var result = await ApiClient.MmoCatchCertificateCasePostWithHttpInfoAsync(ApplicationConstants.ApiVersion1, model);
        return result.StatusCode;
    }
}
