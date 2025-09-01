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

public class StorageDocumentMessageProcessor(
    V3Api.IMmoStorageDocumentApi apiClient,
    ILogger<StorageDocumentMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V3Inbound.StorageDocumentCreateInbound, StandardMessageHeader, V3Api.IMmoStorageDocumentApi, V3ApiModel.StorageDocument>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Storage Document";

    protected override string IdName => nameof(V3Inbound.StorageDocumentCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.StorageDocumentSubmittedMessageLabelPrefix,
        ApplicationConstants.StorageDocumentVoidedMessageLabelPrefix
    };

    protected override string GetId(V3Inbound.StorageDocumentCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V3ApiModel.StorageDocument model)
    {
        model._Version ??= 3;
        var result = await ApiClient.CreateStorageDocumentWithHttpInfoAsync(ApplicationConstants.ApiVersion3, model);
        return result.StatusCode;
    }
}
