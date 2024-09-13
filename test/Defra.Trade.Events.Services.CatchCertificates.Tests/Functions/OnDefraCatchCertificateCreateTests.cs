// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Functions;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Tests.Functions;

public class OnDefraCatchCertificateCreateTests
{
    [Fact]
    public void OnDefraCatchCertificateCreate_Exception_Continue()
    {
        // Arrange
        var message = new Mock<ServiceBusReceivedMessage>();
        var messageReceiver = new Mock<ServiceBusMessageActions>();
        var executionContext = new Mock<ExecutionContext>();
        var logger = new Mock<ILogger>();
        var processorFactory = new Mock<IFesMessageExecutorFactory>();
        var executor = new Mock<IFesMessageExecutor>();

        processorFactory.Setup(f =>
                f.CreateMessageExecutor(It.IsAny<ServiceBusReceivedMessage>()))
            .Returns(executor.Object);

        executor.Setup(e => e.ExecuteAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<ServiceBusMessageActions>(),
                It.IsAny<ExecutionContext>(), It.IsAny<IAsyncCollector<ServiceBusMessage>>()))
            .Throws<InvalidCastException>();

        var sut = new OnDefraCatchCertificateCreate(processorFactory.Object);

        // Act
        var result = sut.RunAsync(message.Object, messageReceiver.Object, executionContext.Object, null, logger.Object);

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
        var executionContext = new Mock<ExecutionContext>();
        var logger = new Mock<ILogger>();
        var processorFactory = new Mock<IFesMessageExecutorFactory>();
        var executor = new Mock<IFesMessageExecutor>();
        processorFactory.Setup(f =>
                f.CreateMessageExecutor(It.IsAny<ServiceBusReceivedMessage>()))
            .Returns(executor.Object);
        var sut = new OnDefraCatchCertificateCreate(processorFactory.Object);

        // Act
        var result = sut.RunAsync(message.Object, messageReceiver.Object, executionContext.Object, null, logger.Object);

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(TaskStatus.RanToCompletion);
    }
}
