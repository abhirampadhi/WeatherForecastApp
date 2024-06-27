//using AutoFixture.Xunit2;
//using Euronext.WeatherForecastApp.Application.Commands.Create;
//using Euronext.WeatherForecastApp.Application.Models.DTOs;
//using Euronext.WeatherForecastApp.Application.Queries;
//using Euronext.WeatherForecastApp.Application.Services;
//using Euronext.WeatherForecastApp.Common.Tests.AutoMoq;
//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;

//namespace Euronext.WeatherForecastApp.Application.Tests.Services;

//public class AuthServiceTests
//{
//    [Theory, AutoMoqData]
//    public async Task LoginAsync_ValidUser_ReturnsUserDto(
//        [Frozen] Mock<IMediator> mediatorMock,
//        [Frozen] Mock<IConfiguration> configurationMock,
//        [Frozen] Mock<ILogger<AuthService>> loggerMock,
//        AuthService authService,
//        LoginUserDto validLoginUser,
//        UserDto mockUser)
//    {
//        // Arrange
//        mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
//                    .ReturnsAsync(mockUser);
//        configurationMock.SetupGet(c => c["JWT:SecretKey"]).Returns("test_secret_key");

//        // Act
//        var result = await authService.LoginAsync(validLoginUser);

//        // Assert
//        result.Should().NotBeNull();
//        result.UserName.Should().Be(mockUser.UserName);
//        result.Token.Should().NotBeNullOrEmpty();
//        loggerMock.Verify(
//            x => x.LogInformation("User {UserName} logged in successfully", validLoginUser.UserName),
//            Times.Once);
//    }

//    [Theory, AutoMoqData]
//    public async Task LoginAsync_InvalidUser_ReturnsNull(
//        [Frozen] Mock<IMediator> mediatorMock,
//        [Frozen] Mock<IConfiguration> configurationMock,
//        [Frozen] Mock<ILogger<AuthService>> loggerMock,
//        AuthService authService,
//        LoginUserDto invalidLoginUser)
//    {
//        // Arrange
//        mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
//                    .ReturnsAsync((UserDto)null);

//        // Act
//        var result = await authService.LoginAsync(invalidLoginUser);

//        // Assert
//        result.Should().BeNull();
//        loggerMock.Verify(
//            x => x.LogWarning("Login failed for user {UserName}", invalidLoginUser.UserName),
//            Times.Once);
//    }

//    [Theory, AutoMoqData]
//    public async Task RegisterAsync_ValidUser_Succeeds(
//        [Frozen] Mock<IMediator> mediatorMock,
//        [Frozen] Mock<ILogger<AuthService>> loggerMock,
//        AuthService authService,
//        RegisterUserDto validRegisterUser)
//    {
//        // Arrange
//        // Assuming no exceptions are thrown
//        mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
//                    .Returns(Task.CompletedTask);

//        // Act
//        Func<Task> act = async () => await authService.RegisterAsync(validRegisterUser);

//        // Assert
//        await act.Should().NotThrowAsync();
//        loggerMock.Verify(
//            x => x.LogInformation("User {UserName} registered successfully", validRegisterUser.UserName),
//            Times.Once);
//    }

//    [Theory, AutoMoqData]
//    public async Task RegisterAsync_ExceptionThrown_LogsError(
//        [Frozen] Mock<IMediator> mediatorMock,
//        [Frozen] Mock<ILogger<AuthService>> loggerMock,
//        AuthService authService,
//        RegisterUserDto invalidRegisterUser)
//    {
//        // Arrange
//        mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
//                    .ThrowsAsync(new Exception("Simulated exception"));

//        // Act
//        Func<Task> act = async () => await authService.RegisterAsync(invalidRegisterUser);

//        // Assert
//        await act.Should().ThrowAsync<Exception>();
//        loggerMock.Verify(
//            x => x.LogError(It.IsAny<Exception>(), "An error occurred while registering user {UserName}", invalidRegisterUser.UserName),
//            Times.Once);
//    }
//}
