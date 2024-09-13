// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Api;
using Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Client;
using Defra.Trade.Common.Exceptions;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Mappers;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V1.MessageProcessors;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using ApiModel = Defra.Trade.Catch.Certificate.Internal.V1INTERNAL.ApiClient.Model;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V1.MessageProcessors;

public class CatchCertificateCaseMessageProcessorTests
{
    private readonly Mock<IMmoCatchCertificateCaseApi> _apiClient = new(MockBehavior.Strict);

    [Fact]
    public async Task BuildCustomMessageHeader_Default_NewHeader()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var result = await sut.BuildCustomMessageHeaderAsync();

        // Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetSchema_Default_EmptyString()
    {
        // Arrange
        var header = new StandardMessageHeader();
        var sut = CreateSut();

        // Act
        string result = await sut.GetSchemaAsync(header);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task ProcessAsync_FailedApiPost_ThrowsException()
    {
        // Arrange
        var inboundModel = new Fixture().Create<CatchCertificateCaseCreateInbound>();
        var messageHeader = new StandardMessageHeader();

        _apiClient
            .Setup(c =>
                c.MmoCatchCertificateCasePostWithHttpInfoAsync(
                    It.IsAny<string>(),
                    It.IsAny<ApiModel.CatchCertificateCase>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResponse<object>(HttpStatusCode.BadRequest, null));

        var sut = CreateSut();

        // Act, Assert
        await Assert.ThrowsAsync<MessageProcessorException>(() => sut.ProcessAsync(inboundModel, messageHeader));
    }

    [Fact]
    public async Task ProcessAsync_SuccessfulApiPost_Success()
    {
        // Arrange
        var inboundModel = new Fixture().Create<CatchCertificateCaseCreateInbound>();
        var messageHeader = new StandardMessageHeader();

        _apiClient
            .Setup(c =>
                c.MmoCatchCertificateCasePostWithHttpInfoAsync(
                "v1-internal",
                It.Is<ApiModel.CatchCertificateCase>(d => d.DocumentNumber == inboundModel.DocumentNumber),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResponse<object>(HttpStatusCode.NoContent, null));

        var sut = CreateSut();

        // Act
        var result = await sut.ProcessAsync(inboundModel, messageHeader);

        // Assert
        result.ShouldNotBeNull();

        _apiClient.Verify(c =>
            c.MmoCatchCertificateCasePostWithHttpInfoAsync(
                "v1-internal",
                It.Is<ApiModel.CatchCertificateCase>(d => d.DocumentNumber == inboundModel.DocumentNumber),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("storage_document_submitted", false)]
    [InlineData("storage_document_voided", false)]
    [InlineData("catch_certificate_submitted", true)]
    [InlineData("catch_certificate_voided", true)]
    [InlineData("processing_statement_submitted", false)]
    [InlineData("processing_statement_voided", false)]
    public async Task ValidateMessageLabel_NotProvided_False(string label, bool expectedResult)
    {
        // Arrange
        var messageHeader = new StandardMessageHeader { Label = label };
        var sut = CreateSut();

        // Act
        bool result = await sut.ValidateMessageLabelAsync(messageHeader);

        // Assert
        result.ShouldBe(expectedResult);
    }

    private CatchCertificateCaseMessageProcessor CreateSut()
    {
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<FesApiProfile>();
        }).CreateMapper();

        return new CatchCertificateCaseMessageProcessor(
            _apiClient.Object,
            Mock.Of<ILogger<CatchCertificateCaseMessageProcessor>>(),
            mapper);
    }
}
