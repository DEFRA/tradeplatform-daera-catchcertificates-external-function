// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;

public class FesMessageExecutorPredicate<T>(Predicate<ServiceBusReceivedMessage> predicate)
    : IFesMessageExecutorFilter where T : IFesMessageExecutor
{
    private readonly Predicate<ServiceBusReceivedMessage> _predicate = predicate;

    public bool CanHandle(ServiceBusReceivedMessage message)
    {
        return _predicate(message);
    }

    public IFesMessageExecutor GetExecutor(IServiceProvider services)
    {
        return services.GetRequiredService<T>();
    }
}
