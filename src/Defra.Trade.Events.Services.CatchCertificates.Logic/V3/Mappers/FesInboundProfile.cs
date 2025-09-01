// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Mappers;

public class FesInboundProfile : Profile
{
    public FesInboundProfile()
    {
        CreateMap<V3Inbound.CatchCertificateCaseCreateInbound, V3Inbound.CatchCertificateCaseCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V3Inbound.ProcessingStatementCreateInbound, V3Inbound.ProcessingStatementCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V3Inbound.StorageDocumentCreateInbound, V3Inbound.StorageDocumentCreateInbound>()
            .ConvertUsing(s => s);
    }
}
