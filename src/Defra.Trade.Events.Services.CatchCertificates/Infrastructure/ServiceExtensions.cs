// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions;
using Defra.Trade.Common.Functions.EventStore;
using Defra.Trade.Common.Functions.Interfaces;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Common.Functions.Validation;
using Defra.Trade.Common.Logging.Extensions;
using Defra.Trade.Common.Security.Authentication.Infrastructure;
using Defra.Trade.Common.Security.Authentication.Interfaces;
using Defra.Trade.Events.Services.CatchCertificates.Logic;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Configuration;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using V1Api = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Api;
using V1Configuration = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Client.Configuration;
using V1Filter = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.MessageFilter;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using V1Processors = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.MessageProcessors;
using V2Api = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Api;
using V2Configuration = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Client.Configuration;
using V2Filter = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.MessageFilter;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;
using V2Processors = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.MessageProcessors;

namespace Defra.Trade.Events.Services.CatchCertificates.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceRegistrations(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddConfig(config)
            .AddMappers()
            .AddValidators()
            .AddEventStoreConfiguration()
            .AddApimAuthentication(config.GetSection(nameof(ApimSettings)))
            .AddApiClients()
            .AddMessagePipelines()
            .AddFunctionLogging(ApplicationConstants.AppName);

        return services;
    }

    private static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        return services
            .AddScoped(CreateV1ApiClientConfig)
            .AddScoped<V1Api.IMmoCatchCertificateCaseApi>(p => new V1Api.MmoCatchCertificateCaseApi(p.GetRequiredService<V1Configuration>()))
            .AddScoped<V1Api.IMmoProcessingStatementApi>(p => new V1Api.MmoProcessingStatementApi(p.GetRequiredService<V1Configuration>()))
            .AddScoped<V1Api.IMmoStorageDocumentApi>(p => new V1Api.MmoStorageDocumentApi(p.GetRequiredService<V1Configuration>()))

            .AddScoped(CreateV2ApiClientConfig)
            .AddScoped<V2Api.IMmoCatchCertificateCaseApi>(p => new V2Api.MmoCatchCertificateCaseApi(p.GetRequiredService<V2Configuration>()))
            .AddScoped<V2Api.IMmoProcessingStatementApi>(p => new V2Api.MmoProcessingStatementApi(p.GetRequiredService<V2Configuration>()))
            .AddScoped<V2Api.IMmoStorageDocumentApi>(p => new V2Api.MmoStorageDocumentApi(p.GetRequiredService<V2Configuration>()));
    }

    private static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddOptions<MmoApiConfig>()
            .Configure<IConfiguration>((apiConfig, configuration) =>
            {
                configuration.GetSection(MmoApiConfig.AppSettingsName).Bind(apiConfig);
            });

        services.Configure<ApimConfiguration>(config.GetSection(ApimConfiguration.OptionsName));

        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(StandardMessageHeader).Assembly, typeof(ApimConfiguration).Assembly);
    }

    private static IServiceCollection AddMessagePipeline<TInbound, THeader, TProcessor>(this IServiceCollection services, Predicate<ServiceBusReceivedMessage> predicate)
        where TProcessor : class, IMessageProcessor<TInbound, THeader>
        where THeader : BaseMessageHeader
    {
        return services.AddMessagePipeline<TInbound, TInbound, TInbound, THeader, TProcessor>(predicate);
    }

    [SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "The types used cannot be reduced further")]
    private static IServiceCollection AddMessagePipeline<TInbound, TDomain, TOutbound, THeader, TProcessor>(this IServiceCollection services, Predicate<ServiceBusReceivedMessage> predicate)
        where TProcessor : class, IMessageProcessor<TDomain, THeader>
        where THeader : BaseMessageHeader
    {
        services.TryAddSingleton<ICustomValidatorFactory, FluentValidatorFactory>();
        services.TryAddSingleton<ISchemaValidator, SchemaValidator>();
        services.TryAddTransient<IInboundMessageValidator<TInbound, THeader>, InboundMessageValidator<TInbound, TDomain, THeader>>();
        services.TryAddTransient<IFesMessageExecutorFactory, FesMessageExecutorFactory>();
        services.AddSingleton<IFesMessageExecutorFilter>(new FesMessageExecutorPredicate<FesMessageExecutor<TInbound>>(predicate));
        services.TryAddTransient<FesMessageExecutor<TInbound>>();
        services.TryAddTransient<IMessageProcessor<TDomain, THeader>, TProcessor>();
        services.TryAddTransient<IBaseMessageProcessorService<TInbound>, BaseMessageProcessorService<TInbound, TOutbound, TDomain, THeader>>();
        return services;
    }

    private static IServiceCollection AddMessagePipelines(this IServiceCollection services)
    {
        return services
            // V1
            .AddMessagePipeline<V1Inbound.CatchCertificateCaseCreateInbound, StandardMessageHeader, V1Processors.CatchCertificateCaseMessageProcessor>(V1Filter.IsCatchCertificateMessage)
            .AddMessagePipeline<V1Inbound.ProcessingStatementCreateInbound, StandardMessageHeader, V1Processors.ProcessingStatementMessageProcessor>(V1Filter.IsProcessingStatementMessage)
            .AddMessagePipeline<V1Inbound.StorageDocumentCreateInbound, StandardMessageHeader, V1Processors.StorageDocumentMessageProcessor>(V1Filter.IsStorageDocumentMessage)

            //V2
            .AddMessagePipeline<V2Inbound.CatchCertificateCaseCreateInbound, StandardMessageHeader, V2Processors.CatchCertificateCaseMessageProcessor>(V2Filter.IsCatchCertificateMessage)
            .AddMessagePipeline<V2Inbound.ProcessingStatementCreateInbound, StandardMessageHeader, V2Processors.ProcessingStatementMessageProcessor>(V2Filter.IsProcessingStatementMessage)
            .AddMessagePipeline<V2Inbound.StorageDocumentCreateInbound, StandardMessageHeader, V2Processors.StorageDocumentMessageProcessor>(V2Filter.IsStorageDocumentMessage);
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<StandardMessageHeader>(lifetime: ServiceLifetime.Singleton)
            .AddValidatorsFromAssemblyContaining<ApimConfiguration>(lifetime: ServiceLifetime.Singleton);
    }

    private static V1Configuration CreateV1ApiClientConfig(IServiceProvider provider)
    {
        var (baseAddress, headers) = GetAuthDetailsAsync(provider).Result;
        return new()
        {
            BasePath = $"{baseAddress}/1-internal",
            DefaultHeaders = headers
        };
    }

    private static V2Configuration CreateV2ApiClientConfig(IServiceProvider provider)
    {
        var (baseAddress, headers) = GetAuthDetailsAsync(provider).Result;
        return new()
        {
            BasePath = $"{baseAddress}/2-internal",
            DefaultHeaders = headers
        };
    }

    private static async Task<AuthDetails> GetAuthDetailsAsync(IServiceProvider provider)
    {
        var authService = provider.GetRequiredService<IAuthenticationService>();
        var apimConfiguration = provider.GetRequiredService<IOptions<ApimConfiguration>>().Value;
        var mmoApiConfig = provider.GetRequiredService<IOptions<MmoApiConfig>>().Value;

        var authToken = await authService.GetAuthenticationHeaderAsync();

        var headers = new Dictionary<string, string>();
        if (authToken?.ToString() is { Length: > 0 } authHeader)
        {
            headers[ApplicationConstants.AuthorizationHeader] = authHeader;
        }

        if (apimConfiguration.SubscriptionKey is { Length: > 0 } apimSubscription)
        {
            headers[ApplicationConstants.ApimSubscriptionKeyHeader] = apimSubscription;
        }

        return new(mmoApiConfig.BaseAddress, headers);
    }

    private readonly record struct AuthDetails(string BaseAddress, Dictionary<string, string> Headers);
}
