// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using AutoMapper;
using V3Api = Defra.Trade.Catch.Certificate.Internal.V3INTERNAL.ApiClient.Model;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Mappers;

public class FesApiProfile : Profile
{
    public FesApiProfile()
    {
        CreateApiMap<V3Inbound.CatchCertificateCaseCreateInbound, V3Api.CatchCertificateCase>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V3Inbound.ProcessingStatementCreateInbound, V3Api.ProcessingStatement>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V3Inbound.StorageDocumentCreateInbound, V3Api.StorageDocument>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => s.DocumentDate))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest._Version, opt => opt.MapFrom(s => s.Version));

        CreateApiMap<V3Inbound.AddressInbound, V3Api.Address>();
        CreateApiMap<V3Inbound.AuditInbound, V3Api.Audit>();
        CreateApiMap<V3Inbound.AuthorityInbound, V3Api.Authority>();
        CreateApiMap<V3Inbound.CatchInbound, V3Api.Catch>();
        CreateApiMap<V3Inbound.CatchValidationInbound, V3Api.CatchValidation>();
        CreateApiMap<V3Inbound.CountryInbound, V3Api.Country>();
        CreateApiMap<V3Inbound.DynamicsAddressInbound, V3Api.DynamicsAddress>(() => new());
        CreateApiMap<V3Inbound.ExporterInbound, V3Api.Exporter>();
        CreateApiMap<V3Inbound.LandingInbound, V3Api.Landing>();
        CreateApiMap<V3Inbound.LandingValidationInbound, V3Api.LandingValidation>();
        CreateApiMap<V3Inbound.ProductInbound, V3Api.Product>();
        CreateApiMap<V3Inbound.ProductValidationInbound, V3Api.ProductValidation>();
        CreateApiMap<V3Inbound.RiskInbound, V3Api.Risk>(() => new());
        CreateApiMap<V3Inbound.StorageFacilityInbound, V3Api.StorageFacility>();
        CreateApiMap<V3Inbound.TransportationInbound, V3Api.Transportation>(() => new());
        CreateMap<V3Inbound.ModeOfTransportInbound, V3Api.ModeOfTransport>();
    }

    private IMappingExpression<TSource, TDestination> CreateApiMap<TSource, TDestination>(Func<TDestination> factory = null)
    {
        factory ??= static () => (TDestination)Activator.CreateInstance(typeof(TDestination), true);
        return CreateMap<TSource, TDestination>()
            .ConstructUsing(_ => factory());
    }
}
