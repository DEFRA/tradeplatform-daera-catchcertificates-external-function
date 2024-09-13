// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Moq;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.MessageExecutors;

public class FesMessageExecutorTests
{
    [Fact]
    public async Task Execute_MessageReceived_CallsProcessor()
    {
        var collector = Mock.Of<IAsyncCollector<ServiceBusMessage>>();
        var message = Mock.Of<ServiceBusReceivedMessage>();
        var messageReceiver = Mock.Of<ServiceBusMessageActions>();
        var messageProcessor = new Mock<IBaseMessageProcessorService<CatchCertificateCaseCreateInbound>>();
        var sut = new FesMessageExecutor<CatchCertificateCaseCreateInbound>(messageProcessor.Object);
        var invocationId = Guid.NewGuid();

        await sut.ExecuteAsync(
            message,
            messageReceiver,
            new ExecutionContext { InvocationId = invocationId },
            collector);

        messageProcessor.Verify(p =>
            p.ProcessAsync(
                invocationId.ToString(),
                "defra.trade.catch.create",
                "CatchCertificates",
                message,
                messageReceiver,
                collector,
                null,
                null,
                "FES",
                null,
                "Create"),
            Times.Once);
    }
}
