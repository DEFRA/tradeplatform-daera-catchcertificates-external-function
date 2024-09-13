// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using Azure.Messaging.ServiceBus;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;

public interface IFesMessageExecutorFilter
{
    bool CanHandle(ServiceBusReceivedMessage message);

    IFesMessageExecutor GetExecutor(IServiceProvider services);
}
