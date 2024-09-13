// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;

public class FesMessageExecutorFactory : IFesMessageExecutorFactory
{
    private readonly IEnumerable<IFesMessageExecutorFilter> _filters;
    private readonly IServiceProvider _serviceProvider;

    public FesMessageExecutorFactory(IEnumerable<IFesMessageExecutorFilter> filters, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(filters);
        ArgumentNullException.ThrowIfNull(serviceProvider);
        _serviceProvider = serviceProvider;
        _filters = filters;
    }

    public IFesMessageExecutor CreateMessageExecutor(ServiceBusReceivedMessage message)
    {
        if (_filters.FirstOrDefault(f => f.CanHandle(message)) is { } filter)
        {
            return filter.GetExecutor(_serviceProvider);
        }

        throw new ArgumentException("Unable to determine a suitable message executor", nameof(message));
    }
}
