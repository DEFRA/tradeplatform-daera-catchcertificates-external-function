// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Isolated;
using Defra.Trade.Common.Functions.Isolated.Interfaces;
using Microsoft.Azure.Functions.Worker;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;

public class FesMessageExecutor<T> : IFesMessageExecutor
{
    private readonly IBaseMessageProcessorService<T> _messageProcessor;

    public FesMessageExecutor(IBaseMessageProcessorService<T> messageProcessor)
    {
        ArgumentNullException.ThrowIfNull(messageProcessor);
        _messageProcessor = messageProcessor;
    }

    public async Task ExecuteAsync(ServiceBusReceivedMessage message, ServiceBusMessageActions messageReceiver,
        FunctionContext executionContext, ServiceBusSender eventStoreSender)
    {
        await _messageProcessor.ProcessAsync(
            executionContext.InvocationId,
            ApplicationConstants.ServiceBus.QueueName.CatchCertificatesCreate,
            ApplicationConstants.AppName,
            message,
            messageReceiver,
            eventStoreSender,
            originalCrmPublisherId: ApplicationConstants.FesAppName,
            originalSource: message.Subject,
            originalRequestName: "Create");
    }
}
