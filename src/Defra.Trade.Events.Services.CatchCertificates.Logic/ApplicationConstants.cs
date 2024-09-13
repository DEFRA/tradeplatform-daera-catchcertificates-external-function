// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic;

public static class ApplicationConstants
{
    public const string ApimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
    public const string ApiVersion1 = "v1-internal";
    public const string ApiVersion2 = "v2-internal";
    public const string AppName = "CatchCertificates";
    public const string AuthorizationHeader = "Authorization";
    public const string CatchCertificateSubmittedMessageLabelPrefix = "catch_certificate_submitted";
    public const string CatchCertificateVoidedMessageLabelPrefix = "catch_certificate_voided";
    public const string FesAppName = "FES";
    public const string ProcessingStatementSubmittedMessageLabelPrefix = "processing_statement_submitted";
    public const string ProcessingStatementVoidedMessageLabelPrefix = "processing_statement_voided";
    public const string StorageDocumentSubmittedMessageLabelPrefix = "storage_document_submitted";
    public const string StorageDocumentVoidedMessageLabelPrefix = "storage_document_voided";
    public const string AppConfigSentinelName = "Sentinel";

    public static class ServiceBus
    {
#if DEBUG

        // In 'Debug' (locally) use connection string
        public const string ConnectionStringConfigurationKey = "ServiceBus:ConnectionString";

#else

        // Assumes that this is 'Release' and uses Managed Identity rather than connection string
        // ie it will actually bind to ServiceBus:FullyQualifiedNamespace !
        public const string ConnectionStringConfigurationKey = "ServiceBus";

#endif

        public static class FunctionName
        {
            public const string CatchCertificateCreate = "on-defra-catch-certificate-create";
        }

        public static class QueueName
        {
            public const string CatchCertificatesCreate = "defra.trade.catch.create";
            public const string DefraTradeEventsInfo = Common.Functions.Constants.QueueName.DefaultEventsInfoQueueName;
        }
    }
}
