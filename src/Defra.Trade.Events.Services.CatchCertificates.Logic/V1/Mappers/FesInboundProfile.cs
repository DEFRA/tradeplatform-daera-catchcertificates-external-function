// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Mappers;

public class FesInboundProfile : Profile
{
    public FesInboundProfile()
    {
        CreateMap<V1Inbound.CatchCertificateCaseCreateInbound, V1Inbound.CatchCertificateCaseCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V1Inbound.ProcessingStatementCreateInbound, V1Inbound.ProcessingStatementCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V1Inbound.StorageDocumentCreateInbound, V1Inbound.StorageDocumentCreateInbound>()
            .ConvertUsing(s => s);
    }
}
