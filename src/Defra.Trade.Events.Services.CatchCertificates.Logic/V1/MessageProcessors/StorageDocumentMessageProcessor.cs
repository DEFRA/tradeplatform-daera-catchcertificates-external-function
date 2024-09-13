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

public class StorageDocumentMessageProcessor(
    V1Api.IMmoStorageDocumentApi apiClient,
    ILogger<StorageDocumentMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V1Inbound.StorageDocumentCreateInbound, StandardMessageHeader, V1Api.IMmoStorageDocumentApi, V1ApiModel.StorageDocument>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Storage Document";

    protected override string IdName => nameof(V1Inbound.StorageDocumentCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.StorageDocumentSubmittedMessageLabelPrefix,
        ApplicationConstants.StorageDocumentVoidedMessageLabelPrefix
    };

    protected override string GetId(V1Inbound.StorageDocumentCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V1ApiModel.StorageDocument model)
    {
        var result = await ApiClient.MmoStorageDocumentPostWithHttpInfoAsync(ApplicationConstants.ApiVersion1, model);
        return result.StatusCode;
    }
}
