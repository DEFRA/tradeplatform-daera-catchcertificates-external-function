// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.Common.AppConfig;
using Defra.Trade.Common.Config;
using Defra.Trade.Common.Function.Health.HealthChecks;
using Defra.Trade.Events.Services.CatchCertificates.Infrastructure;
using Defra.Trade.Events.Services.CatchCertificates.Logic;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Configuration;
using FunctionHealthCheck;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Defra.Trade.Events.Services.CatchCertificates.Logic.ApplicationConstants;

[assembly: FunctionsStartup(typeof(Defra.Trade.Events.Services.CatchCertificates.Startup))]

namespace Defra.Trade.Events.Services.CatchCertificates;

[ExcludeFromCodeCoverage(Justification = "Tested by E2E automation.")]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services.AddServiceRegistrations(configuration);

        var healthChecksBuilder = builder.Services.AddFunctionHealthChecks();
        RegisterHealthChecks(healthChecksBuilder, configuration);
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder
            .ConfigureTradeAppConfiguration(opt =>
            {
                opt.UseKeyVaultSecrets = true;
                opt.RefreshKeys.Add($"{ApplicationConstants.AppName}:{ApplicationConstants.AppConfigSentinelName}");
                opt.Select<ConfigurationServerSettings>(ConfigurationServerSettings.OptionsName);
                opt.Select<ServiceBusSettings>(ServiceBusSettings.OptionsName);
                opt.Select<ApimConfiguration>(ApimConfiguration.OptionsName);
                opt.ConfigServer.Select(ApplicationConstants.AppName);
            });
    }

    private static void RegisterHealthChecks(
        IHealthChecksBuilder builder,
        IConfiguration configuration)
    {
        builder.AddCheck<AppSettingHealthCheck>("ServiceBus:ConnectionString");
        builder.AddAzureServiceBusCheck(configuration, "ServiceBus:ConnectionString", ServiceBus.QueueName.CatchCertificatesCreate);
    }
}
