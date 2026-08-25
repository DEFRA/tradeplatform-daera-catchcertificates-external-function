// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.Common.AppConfig;
using Defra.Trade.Common.Config;
using Defra.Trade.Common.Function.Health.HealthChecks;
using Defra.Trade.Events.Services.CatchCertificates.Infrastructure;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Defra.Trade.Events.Services.CatchCertificates.Logic.ApplicationConstants;

[assembly: ExcludeFromCodeCoverage]

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((_, builder) =>
    {
        builder.ConfigureTradeAppConfiguration(opt =>
        {
            opt.UseKeyVaultSecrets = true;
            opt.RefreshKeys.Add($"{AppName}:{AppConfigSentinelName}");
            opt.Select<ConfigurationServerSettings>(ConfigurationServerSettings.OptionsName);
            opt.Select<ServiceBusSettings>(ServiceBusSettings.OptionsName);
            opt.Select<ApimConfiguration>(ApimConfiguration.OptionsName);
            opt.ConfigServer.Select(AppName);
        });
    })
    .ConfigureServices((context, services) =>
    {
        services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .AddTradeAppConfiguration(context.Configuration)
            .AddServiceRegistrations(context.Configuration);
        services
            .AddHealthChecks()
            .AddCheck<AppSettingHealthCheck>("ServiceBus:ConnectionString");
    })
    .Build();

await host.RunAsync();
