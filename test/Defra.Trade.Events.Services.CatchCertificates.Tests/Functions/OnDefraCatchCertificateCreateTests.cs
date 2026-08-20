// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Functions;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Tests.Functions;

public class OnDefraCatchCertificateCreateTests
{
    private static FunctionContext BuildContext()
    {
        var ctx = new Mock<FunctionContext>();
        ctx.Setup(x => x.InvocationId).Returns("test-id");
        return ctx.Object;
    }

    [Fact]
    public void OnDefraCatchCertificateCreate_Exception_Continue()
    {
        // Arrange
        var message = new Mock<ServiceBusReceivedMessage>();
        var messageReceiver = new Mock<ServiceBusMessageActions>();
        var processorFactory = new Mock<IFesMessageExecutorFactory>();
        var executor = new Mock<IFesMessageExecutor>();
        var senderMock = new Mock<ServiceBusSender>();
        var sbClient = new Mock<ServiceBusClient>();
        sbClient.Setup(c => c.CreateSender(It.IsAny<string>())).Returns(senderMock.Object);

        processorFactory.Setup(f => f.CreateMessageExecutor(It.IsAny<ServiceBusReceivedMessage>()))
            .Returns(executor.Object);
        executor.Setup(e => e.ExecuteAsync(
                It.IsAny<ServiceBusReceivedMessage>(),
                It.IsAny<ServiceBusMessageActions>(),
                It.IsAny<FunctionContext>(),
                It.IsAny<ServiceBusSender>()))
            .Throws<InvalidCastException>();

        var sut = new OnDefraCatchCertificateCreate(processorFactory.Object, Mock.Of<ILogger<OnDefraCatchCertificateCreate>>(), sbClient.Object);

        // Act
        var result = sut.RunAsync(message.Object, messageReceiver.Object, BuildContext());

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(TaskStatus.RanToCompletion);
    }

    [Fact]
    public void OnDefraCatchCertificateCreate_Valid_Success()
    {
        // Arrange
        var message = new Mock<ServiceBusReceivedMessage>();
        var messageReceiver = new Mock<ServiceBusMessageActions>();
        var processorFactory = new Mock<IFesMessageExecutorFactory>();
        var executor = new Mock<IFesMessageExecutor>();
        var senderMock = new Mock<ServiceBusSender>();
        var sbClient = new Mock<ServiceBusClient>();
        sbClient.Setup(c => c.CreateSender(It.IsAny<string>())).Returns(senderMock.Object);

        processorFactory.Setup(f => f.CreateMessageExecutor(It.IsAny<ServiceBusReceivedMessage>()))
            .Returns(executor.Object);

        var sut = new OnDefraCatchCertificateCreate(processorFactory.Object, Mock.Of<ILogger<OnDefraCatchCertificateCreate>>(), sbClient.Object);

        // Act
        var result = sut.RunAsync(message.Object, messageReceiver.Object, BuildContext());

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(TaskStatus.RanToCompletion);
    }
}
