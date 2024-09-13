// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Linq;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Validators;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V1.Validators;

public class ProcessingStatementCreateValidatorTests
{
    private readonly ProcessingStatementCreateValidator _validator;

    public ProcessingStatementCreateValidatorTests()
    {
        _validator = new ProcessingStatementCreateValidator();
    }

    private void Validate_Returns_ErrorMessage(ProcessingStatementCreateInbound processingStatement, string propertyName, string errorMessage)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(processingStatement);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldNotBe(0);
        result.Errors.Exists(x => x.ErrorMessage == errorMessage).ShouldBeTrue();
    }

    private void Validate_Returns_No_ErrorMessage(ProcessingStatementCreateInbound processingStatement, string propertyName)
    {
        // Arrange
        string propertyNameToValidate = propertyName;

        // Act
        var result = _validator.Validate(processingStatement);

        // Assert
        result.Errors.Count(x => x.PropertyName == propertyNameToValidate).ShouldBe(0);
    }

    [Fact]
    public void Should_Validate_PersonResponsible_Is_NotNull_No_ErrorMessage()
    {
        ProcessingStatementCreateInbound processingStatement = new()
        {
            PersonResponsible = "PersonResponsible"
        };
        Validate_Returns_No_ErrorMessage(processingStatement, "PersonResponsible");
    }

    [Fact]
    public void Should_Validate_PersonResponsible_Is_Null_Returns_ErrorMessage()
    {
        ProcessingStatementCreateInbound processingStatement = new();
        Validate_Returns_ErrorMessage(processingStatement, "PersonResponsible", $"'Person Responsible' must not be empty.");
    }

    [Fact]
    public void Should_Validate_PlantName_Is_NotNull_No_ErrorMessage()
    {
        ProcessingStatementCreateInbound processingStatement = new()
        {
            PlantName = "PlantName"
        };
        Validate_Returns_No_ErrorMessage(processingStatement, "PlantName");
    }

    [Fact]
    public void Should_Validate_PlantName_Is_Null_Returns_ErrorMessage()
    {
        ProcessingStatementCreateInbound processingStatement = new();
        Validate_Returns_ErrorMessage(processingStatement, "PlantName", $"'Plant Name' must not be empty.");
    }
}
