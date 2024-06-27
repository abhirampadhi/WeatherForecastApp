using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Interfaces;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Euronext.WeatherForecastApp.WebApi.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<AuthController>> _mockLogger;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<AuthController>>();
        _mockAuthService = new Mock<IAuthService>();

        _authController = new AuthController(
            _mockMediator.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockAuthService.Object
        );
    }

    [Fact]
    public async Task Login_ValidUser_ReturnsOkObjectResult()
    {
        // Arrange
        var loginUserRequest = new LoginUserModelRequest { UserName = "testuser", Password = "password123" };
        var loggedInUserDto = new UserDto { UserName = "testuser", Name = "Test User" };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginUserDto>()))
                        .ReturnsAsync(loggedInUserDto);

        // Act
        var result = await _authController.Login(loginUserRequest);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult.Value.Should().BeEquivalentTo(loggedInUserDto);
    }

    [Fact]
    public async Task Login_InvalidUser_ReturnsBadRequestObjectResult()
    {
        // Arrange
        var loginUserRequest = new LoginUserModelRequest { UserName = "testuser", Password = "password123" };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginUserDto>()))
                        .ReturnsAsync((UserDto)null); // Simulate login failure

        // Act
        var result = await _authController.Login(loginUserRequest);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsOkObjectResult()
    {
        // Arrange
        var registerUserDto = new RegisterUserDto { UserName = "testuser", Name = "Test User", Password = "password123", Roles = new List<string> { "User", "Admin" } };

        _mockMediator.Setup(x => x.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);

        // Act
        var result = await _authController.Register(registerUserDto);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult.Value.Should().BeEquivalentTo(new { Message = "User registered successfully" });
    }

    [Fact]
    public async Task Register_InvalidUser_ReturnsBadRequestObjectResult()
    {
       
        // Act
        var result = await _authController.Register(null);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}
