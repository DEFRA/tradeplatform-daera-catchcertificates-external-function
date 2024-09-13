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

public class StorageDocumentMessageProcessorTests
{
    private readonly Mock<IMmoStorageDocumentApi> _apiClient = new(MockBehavior.Strict);

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
        var inboundModel = new Fixture().Create<StorageDocumentCreateInbound>();
        var messageHeader = new StandardMessageHeader();

        _apiClient
            .Setup(c =>
                c.MmoStorageDocumentPostWithHttpInfoAsync(
                    "v1-internal",
                    It.Is<ApiModel.StorageDocument>(d => d.DocumentNumber == inboundModel.DocumentNumber),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResponse<ApiModel.StorageDocument>(HttpStatusCode.BadRequest, null));

        var sut = CreateSut();

        // Act, Assert
        await Assert.ThrowsAsync<MessageProcessorException>(() => sut.ProcessAsync(inboundModel, messageHeader));
    }

    [Fact]
    public async Task ProcessAsync_SuccessfulApiPost_Success()
    {
        // Arrange
        var inboundModel = new Fixture().Create<StorageDocumentCreateInbound>();
        var messageHeader = new StandardMessageHeader();

        _apiClient
            .Setup(c =>
                c.MmoStorageDocumentPostWithHttpInfoAsync(
                    "v1-internal",
                    It.Is<ApiModel.StorageDocument>(d => d.DocumentNumber == inboundModel.DocumentNumber),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResponse<ApiModel.StorageDocument>(HttpStatusCode.NoContent, null));

        var sut = CreateSut();

        // Act
        var result = await sut.ProcessAsync(inboundModel, messageHeader);

        // Assert
        result.ShouldNotBeNull();

        _apiClient.Verify(c =>
                c.MmoStorageDocumentPostWithHttpInfoAsync(
                    "v1-internal",
                    It.Is<ApiModel.StorageDocument>(d => d.DocumentNumber == inboundModel.DocumentNumber),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("storage_document_submitted", true)]
    [InlineData("storage_document_voided", true)]
    [InlineData("catch_certificate_submitted", false)]
    [InlineData("catch_certificate_voided", false)]
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

    private StorageDocumentMessageProcessor CreateSut()
    {
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<FesApiProfile>();
        }).CreateMapper();

        return new StorageDocumentMessageProcessor(
            _apiClient.Object,
            Mock.Of<ILogger<StorageDocumentMessageProcessor>>(),
            mapper);
    }
}
