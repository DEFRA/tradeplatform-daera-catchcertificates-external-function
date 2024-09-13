// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Extensions;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2;

public static class MessageFilter
{
    public static bool IsCatchCertificateMessage(ServiceBusReceivedMessage message)
    {
        return message.IsV2Message() && message.IsLabelPrefix(ApplicationConstants.CatchCertificateSubmittedMessageLabelPrefix, ApplicationConstants.CatchCertificateVoidedMessageLabelPrefix);
    }

    public static bool IsProcessingStatementMessage(ServiceBusReceivedMessage message)
    {
        return message.IsV2Message() && message.IsLabelPrefix(ApplicationConstants.ProcessingStatementSubmittedMessageLabelPrefix, ApplicationConstants.ProcessingStatementVoidedMessageLabelPrefix);
    }

    public static bool IsStorageDocumentMessage(ServiceBusReceivedMessage message)
    {
        return message.IsV2Message() && message.IsLabelPrefix(ApplicationConstants.StorageDocumentSubmittedMessageLabelPrefix, ApplicationConstants.StorageDocumentVoidedMessageLabelPrefix);
    }

    private static bool IsLabelPrefix(this ServiceBusReceivedMessage message, params string[] prefixes)
    {
        return message.Label() is string label && Array.Exists(prefixes, prefix => label.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsV2Message(this ServiceBusReceivedMessage message)
    {
        return message.SchemaVersion() is "2";
    }
}
