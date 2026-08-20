// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Logic;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Extensions;
using Defra.Trade.Events.Services.CatchCertificates.Logic.MessageExecutors;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.Services.CatchCertificates.Functions;

public class OnDefraCatchCertificateCreate
{
    private readonly IFesMessageExecutorFactory _executorFactory;
    private readonly ILogger<OnDefraCatchCertificateCreate> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    public OnDefraCatchCertificateCreate(IFesMessageExecutorFactory executorFactory, ILogger<OnDefraCatchCertificateCreate> logger, ServiceBusClient serviceBusClient)
    {
        ArgumentNullException.ThrowIfNull(executorFactory);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(serviceBusClient);
        _executorFactory = executorFactory;
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    [Function(ApplicationConstants.ServiceBus.FunctionName.CatchCertificateCreate)]
    public async Task RunAsync(
        [ServiceBusTrigger(ApplicationConstants.ServiceBus.QueueName.CatchCertificatesCreate, Connection = ApplicationConstants.ServiceBus.ConnectionStringConfigurationKey)] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageReceiver,
        FunctionContext executionContext)
    {
        try
        {
            _logger.MessageReceived(
                message.MessageId,
                ApplicationConstants.ServiceBus.FunctionName.CatchCertificateCreate);

            await using var eventStoreSender = _serviceBusClient.CreateSender(ApplicationConstants.ServiceBus.QueueName.DefraTradeEventsInfo);

            await _executorFactory
                .CreateMessageExecutor(message)
                .ExecuteAsync(message, messageReceiver, executionContext, eventStoreSender);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
