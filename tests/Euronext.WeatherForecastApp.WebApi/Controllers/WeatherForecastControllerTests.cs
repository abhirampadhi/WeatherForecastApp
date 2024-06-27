using AutoFixture;
using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Commands.Delete;
using Euronext.WeatherForecastApp.Application.Commands.Update;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Models.Responses;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastAppCommon.Models;
using Euronext.WeatherForecastAppWebApi.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Euronext.WeatherForecastApp.WebApi.Tests.Controllers;

public class WeatherForecastControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
    private readonly WeatherForecastController _controller;
    private readonly IFixture _fixture;

    public WeatherForecastControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WhenForecastsExist()
    {
        // Arrange
        var forecasts = _fixture.Create<List<WeatherForecastDto>>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastsQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(forecasts);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult.Value.Should().BeEquivalentTo(forecasts);
    }

    [Fact]
    public async Task GetAll_ReturnsNotFound_WhenForecastsDoNotExist()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastsQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((List<WeatherForecastDto>)null);

        // Act
        var result = await _controller.GetAll();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenForecastExists()
    {
        // Arrange
        var forecast = _fixture.Create<WeatherForecastDto>();
        var id = _fixture.Create<int>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((WeatherForecastDto)null);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult.Value.Should().BeEquivalentTo(forecast);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenForecastDoesNotExist()
    {
        // Arrange
        var forecast = _fixture.Create<WeatherForecastDto>();
        forecast = null;
        var id = _fixture.Create<int>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(forecast);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetByDate_ReturnsOkResult_WhenForecastExists()
    {
        // Arrange
        var forecasts = _fixture.Create<IEnumerable<WeatherForecastDto>>();
        var date = _fixture.Create<DateTime>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastsByDateQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(forecasts);

        // Act
        var result = await _controller.GetByDate(date);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult.Value.Should().BeEquivalentTo(forecasts);
    }

    [Fact]
    public async Task GetByDate_ReturnsNotFound_WhenForecastDoesNotExist()
    {
        // Arrange
        var forecasts = _fixture.Create<IEnumerable<WeatherForecastDto>>();
        var date = _fixture.Create<DateTime>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetWeatherForecastsByDateQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(forecasts);

        // Act
        var result = await _controller.GetByDate(date);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Post_ReturnsOk_WhenInputModelIsValid()
    {
        // Arrange
        var inputModel = _fixture.Create<List<WeatherForecastRequestModel>>();
        var dtoList = _fixture.Create<List<WeatherForecastDto>>();
        _mockMapper.Setup(m => m.Map<List<WeatherForecastDto>>(inputModel)).Returns(dtoList);

        // Act
        var result = await _controller.Post(inputModel);

        // Assert
        var okResult = result.Should().BeOfType<OkResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        _mockMediator.Verify(m => m.Send(It.IsAny<AddWeatherForecastsCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenInputModelIsNull()
    {
        // Arrange
        List<WeatherForecastRequestModel> inputModel = null;

        // Act
        var result = await _controller.Post(inputModel);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        badRequestResult.Value.Should().BeOfType<ErrorResponse>();
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenInputModelIsValid()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var inputModel = _fixture.Create<WeatherForecastRequestModel>();
        var dto = _fixture.Create<WeatherForecastDto>();
        _mockMapper.Setup(m => m.Map<WeatherForecastDto>(inputModel)).Returns(dto);

        // Act
        var result = await _controller.Put(id, inputModel);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _mockMediator.Verify(m => m.Send(It.IsAny<UpdateWeatherForecastCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenInputModelIsNull()
    {
        // Arrange
        var id = _fixture.Create<int>();
        WeatherForecastRequestModel inputModel = null;

        // Act
        var result = await _controller.Put(id, inputModel);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        badRequestResult.Value.Should().BeOfType<ErrorResponse>();
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenForecastExists()
    {
        // Arrange
        var id = _fixture.Create<int>();

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _mockMediator.Verify(m => m.Send(It.IsAny<DeleteWeatherForecastCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenForecastDoesNotExist()
    {
        // Arrange
        var id = _fixture.Create<int>();
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteWeatherForecastCommand>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}