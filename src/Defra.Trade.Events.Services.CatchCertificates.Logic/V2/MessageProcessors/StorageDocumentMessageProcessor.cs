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

public class StorageDocumentMessageProcessor(
    V2Api.IMmoStorageDocumentApi apiClient,
    ILogger<StorageDocumentMessageProcessor> logger,
    IMapper mapper) : BaseApiMessageProcessor<V2Inbound.StorageDocumentCreateInbound, StandardMessageHeader, V2Api.IMmoStorageDocumentApi, V2ApiModel.StorageDocument>(apiClient, logger, mapper)
{
    protected override string EntityType => "FES Storage Document";

    protected override string IdName => nameof(V2Inbound.StorageDocumentCreateInbound.DocumentNumber);

    protected override IEnumerable<string> LabelPrefixes { get; } = new[]
    {
        ApplicationConstants.StorageDocumentSubmittedMessageLabelPrefix,
        ApplicationConstants.StorageDocumentVoidedMessageLabelPrefix
    };

    protected override string GetId(V2Inbound.StorageDocumentCreateInbound inbound) => inbound.DocumentNumber;

    protected override async Task<HttpStatusCode> SendAsync(V2ApiModel.StorageDocument model)
    {
        model._Version ??= 2;
        var result = await ApiClient.CreateStorageDocumentWithHttpInfoAsync(ApplicationConstants.ApiVersion2, model);
        return result.StatusCode;
    }
}
