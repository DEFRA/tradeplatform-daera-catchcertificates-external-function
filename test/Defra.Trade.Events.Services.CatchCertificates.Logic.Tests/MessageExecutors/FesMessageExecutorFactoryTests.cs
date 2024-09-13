// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Moq;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.MessageExecutors;

public class FesMessageExecutorFactoryTests
{
    private readonly List<Mock<IFesMessageExecutorFilter>> _filters;
    private readonly Mock<IServiceProvider> _serviceProvider;
    private readonly FesMessageExecutorFactory _sut;

    public FesMessageExecutorFactoryTests()
    {
        _filters = [];
        _serviceProvider = new(MockBehavior.Strict);
        _sut = new(_filters.Select(f => f.Object), _serviceProvider.Object);
    }

    [Fact]
    public void CreateMessageExecutor_Subject_CorrectHandler()
    {
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage();
        var ignoreFilter = new Mock<IFesMessageExecutorFilter>(MockBehavior.Strict);
        var acceptFilter = new Mock<IFesMessageExecutorFilter>(MockBehavior.Strict);
        var skipFilter = new Mock<IFesMessageExecutorFilter>(MockBehavior.Strict);
        var expected = Mock.Of<IFesMessageExecutor>(MockBehavior.Strict);

        ignoreFilter.Setup(m => m.CanHandle(message)).Returns(false).Verifiable();
        acceptFilter.Setup(m => m.CanHandle(message)).Returns(true).Verifiable();
        acceptFilter.Setup(m => m.GetExecutor(_serviceProvider.Object)).Returns(expected);

        _filters.Add(ignoreFilter);
        _filters.Add(ignoreFilter);
        _filters.Add(acceptFilter);
        _filters.Add(skipFilter);

        var result = _sut.CreateMessageExecutor(message);

        result.ShouldBe(expected);
        Mock.Verify(ignoreFilter, acceptFilter, skipFilter);
    }

    [Fact]
    public void CreateMessageExecutor_Unknown_ThrowsException()
    {
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage();

        Assert.Throws<ArgumentException>(() =>
            _sut.CreateMessageExecutor(message));
    }
}
