// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using AutoMapper;
using V2Api = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Model;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Mappers;

public class FesApiProfile : Profile
{
    public FesApiProfile()
    {
        CreateApiMap<V2Inbound.CatchCertificateCaseCreateInbound, V2Api.CatchCertificateCase>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V2Inbound.ProcessingStatementCreateInbound, V2Api.ProcessingStatement>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V2Inbound.StorageDocumentCreateInbound, V2Api.StorageDocument>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V3Inbound.AddressInbound, V2Api.Address>();
        CreateApiMap<V3Inbound.AuditInbound, V2Api.Audit>();
        CreateApiMap<V3Inbound.AuthorityInbound, V2Api.Authority>();
        CreateApiMap<V3Inbound.CatchInbound, V2Api.Catch>();
        CreateApiMap<V3Inbound.CatchValidationInbound, V2Api.CatchValidation>();
        CreateApiMap<V3Inbound.CountryInbound, V2Api.Country>();
        CreateApiMap<V3Inbound.DynamicsAddressInbound, V2Api.DynamicsAddress>(() => new());
        CreateApiMap<V3Inbound.ExporterInbound, V2Api.Exporter>();
        CreateApiMap<V3Inbound.LandingInbound, V2Api.Landing>();
        CreateApiMap<V3Inbound.LandingValidationInbound, V2Api.LandingValidation>();
        CreateApiMap<V3Inbound.ProductInbound, V2Api.Product>();
        CreateApiMap<V3Inbound.ProductValidationInbound, V2Api.ProductValidation>();
        CreateApiMap<V3Inbound.RiskInbound, V2Api.Risk>(() => new());
        CreateApiMap<V3Inbound.StorageFacilityInbound, V2Api.StorageFacility>();
        CreateApiMap<V3Inbound.TransportationInbound, V2Api.Transportation>(() => new());
        CreateMap<V3Inbound.ModeOfTransportInbound, V2Api.ModeOfTransport>();
    }

    private IMappingExpression<TSource, TDestination> CreateApiMap<TSource, TDestination>(Func<TDestination> factory = null)
    {
        factory ??= static () => (TDestination)Activator.CreateInstance(typeof(TDestination), true);
        return CreateMap<TSource, TDestination>()
            .ConstructUsing(_ => factory());
    }
}
