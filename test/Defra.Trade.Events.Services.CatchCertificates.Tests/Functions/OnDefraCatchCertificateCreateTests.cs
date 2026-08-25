// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Functions;
using Defra.Trade.Events.Services.CatchCertificates.Logic;
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
    public void Constructor_NullExecutorFactory_ThrowsArgumentNullException()
    {
        // Arrange
        var sbClient = new Mock<ServiceBusClient>();

        // Act
        var act = () => new OnDefraCatchCertificateCreate(
            null!,
            Mock.Of<ILogger<OnDefraCatchCertificateCreate>>(),
            sbClient.Object);

        // Assert
        Should.Throw<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_NullLogger_ThrowsArgumentNullException()
    {
        // Arrange
        var processorFactory = new Mock<IFesMessageExecutorFactory>();
        var sbClient = new Mock<ServiceBusClient>();

        // Act
        var act = () => new OnDefraCatchCertificateCreate(
            processorFactory.Object,
            null!,
            sbClient.Object);

        // Assert
        Should.Throw<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_NullServiceBusClient_ThrowsArgumentNullException()
    {
        // Arrange
        var processorFactory = new Mock<IFesMessageExecutorFactory>();

        // Act
        var act = () => new OnDefraCatchCertificateCreate(
            processorFactory.Object,
            Mock.Of<ILogger<OnDefraCatchCertificateCreate>>(),
            null!);

        // Assert
        Should.Throw<ArgumentNullException>(act);
    }

    [Fact]
    public void RunAsync_HasFunctionAttribute()
    {
        // Arrange & Act
        var attribute = Defra.Trade.Events.Services.CatchCertificates.Tests.Helpers.FunctionTestHelpers
            .MethodHasSingleAttribute<OnDefraCatchCertificateCreate, FunctionAttribute>(nameof(OnDefraCatchCertificateCreate.RunAsync));

        // Assert
        attribute.Name.ShouldBe(ApplicationConstants.ServiceBus.FunctionName.CatchCertificateCreate);
    }

    [Fact]
    public void RunAsync_HasServiceBusTriggerAttributeWithCorrectValues()
    {
        // Arrange
        var methodInfo = typeof(OnDefraCatchCertificateCreate).GetMethod(nameof(OnDefraCatchCertificateCreate.RunAsync));

        // Act
        var messageParam = methodInfo!.GetParameters()
            .Single(x => x.ParameterType == typeof(ServiceBusReceivedMessage));

        var triggerAttribute = messageParam
            .GetCustomAttributes(typeof(ServiceBusTriggerAttribute), false)
            .Select(x => x as ServiceBusTriggerAttribute)
            .Single();

        // Assert
        triggerAttribute.ShouldNotBeNull();
        triggerAttribute!.QueueName.ShouldBe(ApplicationConstants.ServiceBus.QueueName.CatchCertificatesCreate);
        triggerAttribute.Connection.ShouldBe(ApplicationConstants.ServiceBus.ConnectionStringConfigurationKey);
    }

    [Fact]
    public async Task RunAsync_Valid_CreatesSenderForEventStoreQueueAndExecutesMessage()
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
        await sut.RunAsync(message.Object, messageReceiver.Object, BuildContext());

        // Assert
        sbClient.Verify(c => c.CreateSender(ApplicationConstants.ServiceBus.QueueName.DefraTradeEventsInfo), Times.Once);
        processorFactory.Verify(f => f.CreateMessageExecutor(message.Object), Times.Once);
        executor.Verify(
            e => e.ExecuteAsync(message.Object, messageReceiver.Object, It.IsAny<FunctionContext>(), senderMock.Object),
            Times.Once);
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
