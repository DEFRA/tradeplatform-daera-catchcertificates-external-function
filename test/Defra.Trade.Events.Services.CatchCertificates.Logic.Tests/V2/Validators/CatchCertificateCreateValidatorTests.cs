// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Linq;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Validators;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V2.Validators;

public class CatchCertificateCreateValidatorTests
{
    private readonly CatchCertificateCreateValidator _validator;

    public CatchCertificateCreateValidatorTests()
    {
        _validator = new CatchCertificateCreateValidator();
    }

    [Fact]
    public void Should_Validate__correlationId_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            CorrelationId = Guid.NewGuid()
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "CorrelationId");
    }

    [Fact]
    public void Should_Validate__correlationId_Is_Null_Returns_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new();
        Validate_Returns_ErrorMessage(catchCertificate, "CorrelationId", $"'Correlation Id' must not be empty.");
    }

    [Fact]
    public void Should_Validate_CaseType1_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            CaseType1 = "CaseType1"
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "CaseType1");
    }

    //CaseType1
    [Fact]
    public void Should_Validate_CaseType1_Is_Null_Returns_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new();
        Validate_Returns_ErrorMessage(catchCertificate, "CaseType1", $"'Case Type1' must not be empty.");
    }

    [Fact]
    public void Should_Validate_CaseType2_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            CaseType2 = "CaseType2"
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "CaseType2");
    }

    //CaseType2
    [Fact]
    public void Should_Validate_CaseType2_Is_Null_Returns_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new();
        Validate_Returns_ErrorMessage(catchCertificate, "CaseType2", $"'Case Type2' must not be empty.");
    }

    [Fact]
    public void Should_Validate_DocumentNumber_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            DocumentNumber = Guid.NewGuid().ToString()
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "DocumentNumber");
    }

    [Fact]
    public void Should_Validate_DocumentNumber_Is_Null_Returns_ErrorMessage()
    {
        Validate_Returns_ErrorMessage(new CatchCertificateCaseCreateInbound(), "DocumentNumber", $"Document Number cannot be null");
    }

    [Fact]
    public void Should_Validate_Exporter_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            Exporter = new ExporterInbound()
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "Exporter");
    }

    //Exporter
    [Fact]
    public void Should_Validate_Exporter_Is_Null_Returns_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new();
        Validate_Returns_ErrorMessage(catchCertificate, "Exporter", $"'Exporter' must not be empty.");
    }

    [Fact]
    public void Should_Validate_NumberOfFailedSubmissions_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            NumberOfFailedSubmissions = 1
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "NumberOfFailedSubmissions");
    }

    [Fact]
    public void Should_Validate_RequestedByAdmin_Is_NotNull_No_ErrorMessage()
    {
        CatchCertificateCaseCreateInbound catchCertificate = new()
        {
            RequestedByAdmin = true
        };
        Validate_Returns_No_ErrorMessage(catchCertificate, "RequestedByAdmin");
    }

    private void Validate_Returns_ErrorMessage(CatchCertificateCaseCreateInbound catchCertificate, string propertyName, string errorMessage)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(catchCertificate);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldNotBe(0);
        result.Errors.Exists(x => x.ErrorMessage == errorMessage).ShouldBeTrue();
    }

    private void Validate_Returns_No_ErrorMessage(CatchCertificateCaseCreateInbound catchCertificate, string propertyName)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(catchCertificate);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldBe(0);
    }
}
