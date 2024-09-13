// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1;
using FluentAssertions;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V1;

public static class MessageFilterTests
{
    private static readonly string[] _allowedVersions = [null, "1", ""];

    private static readonly string[] _catchCertificateLabels =
    [
        "catch_certificate_submitted",
        "catch_certificate_submitted-abc",
        "catch_certificate_voided",
        "catch_certificate_voided-abc"
    ];

    private static readonly string[] _disallowedLabels =
    [
        null,
        "",
        "abc"
    ];

    private static readonly string[] _disallowedVersions = ["2", "abc"];

    private static readonly string[] _processingStatementLabels =
    [
        "processing_statement_submitted",
        "processing_statement_submitted-abc",
        "processing_statement_voided",
        "processing_statement_voided-abc"
    ];

    private static readonly string[] _storageDocumentLabels =
    [
        "storage_document_submitted",
        "storage_document_submitted-abc",
        "storage_document_voided",
        "storage_document_voided-abc"
    ];

    [Theory]
    [MemberData(nameof(IsCatchCertificateMessage_Handles_TestCases))]
    public static void IsCatchCertificateMessage_Handles(string schemaVersion, string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(subject: label, properties: new Dictionary<string, object>
        {
            ["SchemaVersion"] = schemaVersion
        });

        // act
        bool actual = MessageFilter.IsCatchCertificateMessage(message);

        // assert
        actual.Should().Be(expected);
    }

    public static TheoryData<string, string, bool> IsCatchCertificateMessage_Handles_TestCases()
    {
        return GenerateTestCases(
            allowedVersions: _allowedVersions,
            disallowedVersions: _disallowedVersions,
            allowedLabels: _catchCertificateLabels,
            disallowedLabels: _disallowedLabels.Concat(_processingStatementLabels).Concat(_storageDocumentLabels));
    }

    [Theory]
    [MemberData(nameof(IsProcessingStatementMessage_Handles_TestCases))]
    public static void IsProcessingStatementMessage_Handles(string schemaVersion, string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(subject: label, properties: new Dictionary<string, object>
        {
            ["SchemaVersion"] = schemaVersion
        });

        // act
        bool actual = MessageFilter.IsProcessingStatementMessage(message);

        // assert
        actual.Should().Be(expected);
    }

    public static TheoryData<string, string, bool> IsProcessingStatementMessage_Handles_TestCases()
    {
        return GenerateTestCases(
            allowedVersions: _allowedVersions,
            disallowedVersions: _disallowedVersions,
            allowedLabels: _processingStatementLabels,
            disallowedLabels: _disallowedLabels.Concat(_catchCertificateLabels).Concat(_storageDocumentLabels));
    }

    [Theory]
    [MemberData(nameof(IsStorageDocumentMessage_Handles_TestCases))]
    public static void IsStorageDocumentMessage_Handles(string schemaVersion, string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(subject: label, properties: new Dictionary<string, object>
        {
            ["SchemaVersion"] = schemaVersion
        });

        // act
        bool actual = MessageFilter.IsStorageDocumentMessage(message);

        // assert
        actual.Should().Be(expected);
    }

    public static TheoryData<string, string, bool> IsStorageDocumentMessage_Handles_TestCases()
    {
        return GenerateTestCases(
            allowedVersions: _allowedVersions,
            disallowedVersions: _disallowedVersions,
            allowedLabels: _storageDocumentLabels,
            disallowedLabels: _disallowedLabels.Concat(_catchCertificateLabels).Concat(_processingStatementLabels));
    }

    private static TheoryData<string, string, bool> GenerateTestCases(
        IEnumerable<string> allowedLabels,
        IEnumerable<string> disallowedLabels,
        IEnumerable<string> allowedVersions,
        IEnumerable<string> disallowedVersions)
    {
        var result = new TheoryData<string, string, bool>();
        AddRange(allowedVersions, allowedLabels, true);
        AddRange(allowedVersions, disallowedLabels, false);
        AddRange(disallowedVersions, allowedLabels, false);
        AddRange(disallowedVersions, disallowedLabels, false);
        return result;

        void AddRange(IEnumerable<string> versions, IEnumerable<string> labels, bool allowed)
        {
            foreach (string version in versions)
            {
                foreach (string label in labels)
                {
                    result.Add(version, label, allowed);
                }
            }
        }
    }
}
