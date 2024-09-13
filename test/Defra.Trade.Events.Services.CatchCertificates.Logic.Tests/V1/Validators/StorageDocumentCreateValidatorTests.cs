// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Linq;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Validators;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V1.Validators;

public class StorageDocumentCreateValidatorTests
{
    private readonly StorageDocumentCreateValidator _validator;

    public StorageDocumentCreateValidatorTests()
    {
        _validator = new StorageDocumentCreateValidator();
    }

    private void Validate_Returns_ErrorMessage(StorageDocumentCreateInbound storageDocument, string propertyName, string errorMessage)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(storageDocument);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldNotBe(0);
        result.Errors.Exists(x => x.ErrorMessage == errorMessage).ShouldBeTrue();
    }

    private void Validate_Returns_No_ErrorMessage(StorageDocumentCreateInbound storageDocument, string propertyName)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(storageDocument);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldBe(0);
    }

    [Fact]
    public void Should_Validate_CompanyName_Is_NotNull_No_ErrorMessage()
    {
        StorageDocumentCreateInbound processingStatement = new()
        {
            CompanyName = "CompanyName"
        };
        Validate_Returns_No_ErrorMessage(processingStatement, "CompanyName");
    }

    [Fact]
    public void Should_Validate_CompanyName_Is_Null_Returns_ErrorMessage()
    {
        StorageDocumentCreateInbound storageDocument = new();
        Validate_Returns_ErrorMessage(storageDocument, "CompanyName", $"'Company Name' must not be empty.");
    }
}
