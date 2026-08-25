// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Isolated;
using Defra.Trade.Common.Functions.Isolated.Interfaces;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;
using Microsoft.Azure.Functions.Worker;
using Moq;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.MessageExecutors;

public class FesMessageExecutorTests
{
    [Fact]
    public async Task Execute_MessageReceived_CallsProcessor()
    {
        var sender = new Mock<ServiceBusSender>().Object;
        var message = Mock.Of<ServiceBusReceivedMessage>();
        var messageReceiver = Mock.Of<ServiceBusMessageActions>();
        var messageProcessor = new Mock<IBaseMessageProcessorService<CatchCertificateCaseCreateInbound>>();
        var sut = new FesMessageExecutor<CatchCertificateCaseCreateInbound>(messageProcessor.Object);
        var invocationId = "test-invocation-id";

        var context = new Mock<FunctionContext>();
        context.Setup(x => x.InvocationId).Returns(invocationId);

        await sut.ExecuteAsync(message, messageReceiver, context.Object, sender);

        messageProcessor.Verify(p =>
            p.ProcessAsync(
                invocationId,
                "defra.trade.catch.create",
                "CatchCertificates",
                message,
                messageReceiver,
                sender,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }
}
