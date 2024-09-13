// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions;
using Defra.Trade.Common.Security.Authentication.Interfaces;
using Defra.Trade.Events.Services.CatchCertificates.Infrastructure;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Tests.Infrastructure;

public static class ServiceExtensionsTests
{
    [Fact]
    public static void CreatesAValidServiceProvider()
    {
        // arrange
        var services = new ServiceCollection();
        var configBuilder = new ConfigurationBuilder();

        var config = configBuilder.Build();

        services.AddLogging();
        services.AddSingleton<IConfiguration>(config);

        // act
        services.AddServiceRegistrations(config);

        // assert
        services.Should().Contain(d => d.ServiceType == typeof(IAuthenticationService));
        services.Replace(new(typeof(IAuthenticationService), typeof(DummyAuth), ServiceLifetime.Singleton));
        var provider = services.BuildServiceProvider(new ServiceProviderOptions()
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });

        var messageProcessorTypes = services.Select(s => s.ServiceType).Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IBaseMessageProcessorService<>));
        foreach (var type in messageProcessorTypes)
        {
            using var scope = provider.CreateScope();
            scope.ServiceProvider.GetRequiredService(type).Should().NotBeNull();
        }
    }

    [Theory]
    [InlineData("catch_certificate_submitted", null, typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted", null, typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted", null, typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted", "", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted", "", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted", "", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted", "1", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted", "1", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted", "1", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted", "2", typeof(V2Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted", "2", typeof(V2Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted", "2", typeof(V2Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted_abcdef", null, typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted_abcdef", null, typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted_abcdef", null, typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted_abcdef", "", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted_abcdef", "", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted_abcdef", "", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted_abcdef", "1", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted_abcdef", "1", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted_abcdef", "1", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_submitted_abcdef", "2", typeof(V2Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_submitted_abcdef", "2", typeof(V2Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_submitted_abcdef", "2", typeof(V2Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided", null, typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided", null, typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided", null, typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided", "", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided", "", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided", "", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided", "1", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided", "1", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided", "1", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided", "2", typeof(V2Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided", "2", typeof(V2Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided", "2", typeof(V2Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided_abcdef", null, typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided_abcdef", null, typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided_abcdef", null, typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided_abcdef", "", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided_abcdef", "", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided_abcdef", "", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided_abcdef", "1", typeof(V1Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided_abcdef", "1", typeof(V1Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided_abcdef", "1", typeof(V1Inbound.StorageDocumentCreateInbound))]
    [InlineData("catch_certificate_voided_abcdef", "2", typeof(V2Inbound.CatchCertificateCaseCreateInbound))]
    [InlineData("processing_statement_voided_abcdef", "2", typeof(V2Inbound.ProcessingStatementCreateInbound))]
    [InlineData("storage_document_voided_abcdef", "2", typeof(V2Inbound.StorageDocumentCreateInbound))]
    public static void RegistersAFesMessageExecutorFactory_WhichCanHandle(string label, string version, Type messageType)
    {
        // arrange
        var services = new ServiceCollection();
        var configBuilder = new ConfigurationBuilder();
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(subject: label, properties: new Dictionary<string, object>
        {
            ["SchemaVersion"] = version
        });

        var config = configBuilder.Build();

        services.AddLogging();
        services.AddSingleton<IConfiguration>(config);

        // act
        services.AddServiceRegistrations(config);
        services.Replace(new(typeof(IAuthenticationService), typeof(DummyAuth), ServiceLifetime.Singleton));
        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IFesMessageExecutorFactory>();

        // assert
        factory.CreateMessageExecutor(message).Should().BeOfType(typeof(FesMessageExecutor<>).MakeGenericType(messageType));
    }

    [Theory]
    [InlineData("catch_certificate_submitted", "3")]
    [InlineData("processing_statement_submitted", "3")]
    [InlineData("storage_document_submitted", "3")]
    [InlineData("catch_certificate_voided", "3")]
    [InlineData("processing_statement_voided", "3")]
    [InlineData("storage_document_voided", "3")]
    [InlineData("abc", null)]
    [InlineData("abc", "1")]
    [InlineData("abc", "2")]
    public static void RegistersAFesMessageExecutorFactory_WhichCantHandle(string label, string version)
    {
        // arrange
        var services = new ServiceCollection();
        var configBuilder = new ConfigurationBuilder();
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(subject: label, properties: new Dictionary<string, object>
        {
            ["SchemaVersion"] = version
        });

        var config = configBuilder.Build();

        services.AddLogging();
        services.AddSingleton<IConfiguration>(config);

        // act
        services.AddServiceRegistrations(config);
        services.Replace(new(typeof(IAuthenticationService), typeof(DummyAuth), ServiceLifetime.Singleton));
        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IFesMessageExecutorFactory>();

        // assert
        var test = () => factory.CreateMessageExecutor(message);
        test.Should().Throw<ArgumentException>().WithMessage("Unable to determine a suitable message executor (Parameter 'message')").WithParameterName("message");
    }

    private class DummyAuth : IAuthenticationService
    {
        public Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new AuthenticationHeaderValue("Bearer", "abc"));
        }
    }
}
