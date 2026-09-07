// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;

public interface IFesMessageExecutor
{
    Task ExecuteAsync(ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageReceiver,
        FunctionContext executionContext,
        ServiceBusSender eventStoreSender);
}
