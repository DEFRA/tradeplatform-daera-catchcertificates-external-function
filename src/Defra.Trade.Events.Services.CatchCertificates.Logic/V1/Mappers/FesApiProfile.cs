// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using AutoMapper;
using V1Api = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Model;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Mappers;

public class FesApiProfile : Profile
{
    public FesApiProfile()
    {
        CreateApiMap<V1Inbound.CatchCertificateCaseCreateInbound, V1Api.CatchCertificateCase>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => DateTime.UtcNow))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName));
        CreateApiMap<V1Inbound.ProcessingStatementCreateInbound, V1Api.ProcessingStatement>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => DateTime.UtcNow))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName));
        CreateApiMap<V1Inbound.StorageDocumentCreateInbound, V1Api.StorageDocument>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(s => DateTime.UtcNow))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(s => ApplicationConstants.FesAppName))
            .ForMember(dest => dest.LastUpdatedSystem, opt => opt.MapFrom(s => ApplicationConstants.FesAppName));
        CreateApiMap<V1Inbound.AuditInbound, V1Api.Audit>();
        CreateApiMap<V1Inbound.AddressInbound, V1Api.Address>();
        CreateApiMap<V1Inbound.DynamicsAddressInbound, V1Api.DynamicsAddress>(() => new());
        CreateApiMap<V1Inbound.ExporterInbound, V1Api.Exporter>();
        CreateApiMap<V1Inbound.LandingInbound, V1Api.Landing>();
        CreateApiMap<V1Inbound.CountryInbound, V1Api.Country>(() => new());
        CreateApiMap<V1Inbound.RiskingInbound, V1Api.Risk>(() => new());
        CreateApiMap<V1Inbound.LandingValidationInbound, V1Api.LandingValidation>();
        CreateApiMap<V1Inbound.ProductInbound, V1Api.Product>();
        CreateApiMap<V1Inbound.ProductsValidationInbound, V1Api.ProductValidation>();
        CreateApiMap<V1Inbound.CatchInbound, V1Api.Catch>();
        CreateApiMap<V1Inbound.CatchValidationInbound, V1Api.CatchValidation>();
    }

    private IMappingExpression<TSource, TDestination> CreateApiMap<TSource, TDestination>(Func<TDestination> factory = null)
    {
        factory ??= static () => (TDestination)Activator.CreateInstance(typeof(TDestination), true);
        return CreateMap<TSource, TDestination>()
            .ConstructUsing(_ => factory());
    }
}
