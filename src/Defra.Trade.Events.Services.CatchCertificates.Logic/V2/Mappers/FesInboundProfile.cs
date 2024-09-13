// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Mappers;

public class FesInboundProfile : Profile
{
    public FesInboundProfile()
    {
        CreateMap<V2Inbound.CatchCertificateCaseCreateInbound, V2Inbound.CatchCertificateCaseCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V2Inbound.ProcessingStatementCreateInbound, V2Inbound.ProcessingStatementCreateInbound>()
            .ConvertUsing(s => s);

        CreateMap<V2Inbound.StorageDocumentCreateInbound, V2Inbound.StorageDocumentCreateInbound>()
            .ConvertUsing(s => s);
    }
}
