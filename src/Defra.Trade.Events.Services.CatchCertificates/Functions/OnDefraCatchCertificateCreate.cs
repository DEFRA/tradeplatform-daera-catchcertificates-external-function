// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Logic;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.Services.CatchCertificates.Functions;

public class OnDefraCatchCertificateCreate
{
    private readonly IFesMessageExecutorFactory _executorFactory;

    public OnDefraCatchCertificateCreate(IFesMessageExecutorFactory executorFactory)
    {
        ArgumentNullException.ThrowIfNull(executorFactory);
        _executorFactory = executorFactory;
    }

    [ServiceBusAccount(ApplicationConstants.ServiceBus.ConnectionStringConfigurationKey)]
    [FunctionName(ApplicationConstants.ServiceBus.FunctionName.CatchCertificateCreate)]
    public async Task RunAsync(
        [ServiceBusTrigger(ApplicationConstants.ServiceBus.QueueName.CatchCertificatesCreate)] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageReceiver,
        ExecutionContext executionContext,
        [ServiceBus(ApplicationConstants.ServiceBus.QueueName.DefraTradeEventsInfo)] IAsyncCollector<ServiceBusMessage> eventStoreCollector,
        ILogger logger)
    {
        try
        {
            await _executorFactory
                .CreateMessageExecutor(message)
                .ExecuteAsync(message, messageReceiver, executionContext, eventStoreCollector);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}
