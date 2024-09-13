// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions;
using Defra.Trade.Common.Functions.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;

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
        ExecutionContext executionContext, IAsyncCollector<ServiceBusMessage> eventStoreCollector)
    {
        await _messageProcessor.ProcessAsync(
            executionContext.InvocationId.ToString(),
            ApplicationConstants.ServiceBus.QueueName.CatchCertificatesCreate,
            ApplicationConstants.AppName,
            message,
            messageReceiver,
            eventStoreCollector,
            originalCrmPublisherId: ApplicationConstants.FesAppName,
            originalSource: message.Label(),
            originalRequestName: "Create");
    }
}
