// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(MessageReceived),
        Level = LogLevel.Information,
        Message = "Message Id : {MessageId} received on {FunctionName}")]
    public static partial void MessageReceived(this ILogger logger, string messageId, string functionName);

    [LoggerMessage(
        EventId = 10,
        EventName = nameof(ProcessorCreate),
        Level = LogLevel.Information,
        Message = "Create {EntityType} with document number: {DocumentNumber}")]
    public static partial void ProcessorCreate(this ILogger logger, string entityType, string documentNumber);

    [LoggerMessage(
        EventId = 11,
        EventName = nameof(ProcessorCreateSuccess),
        Level = LogLevel.Information,
        Message = "Successfully created {EntityType} with document number: {DocumentNumber}")]
    public static partial void ProcessorCreateSuccess(this ILogger logger, string entityType, string documentNumber);

    [LoggerMessage(
        EventId = 12,
        EventName = nameof(ProcessorSend),
        Level = LogLevel.Information,
        Message = "Sending {EntityType} with document number: {DocumentNumber}")]
    public static partial void ProcessorSend(this ILogger logger, string entityType, string documentNumber);

    [LoggerMessage(
        EventId = 13,
        EventName = nameof(ProcessorSendSuccess),
        Level = LogLevel.Information,
        Message = "Successfully sent {EntityType} with document number: {DocumentNumber}")]
    public static partial void ProcessorSendSuccess(this ILogger logger, string entityType, string documentNumber);
}
