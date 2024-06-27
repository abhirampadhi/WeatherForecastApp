using AutoFixture;
using AutoFixture.Xunit2;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastApp.Application.Services;
using Euronext.WeatherForecastApp.Common.JwtWrapper;
using Euronext.WeatherForecastApp.Common.Tests.AutoMoq;
using FluentAssertions;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace Euronext.WeatherForecastApp.Application.Tests.Services;

public class AuthServiceTests
{
    [Theory, AutoMoqData]
    public async Task LoginAsync_ValidUser_ReturnsUserDto(
          [Frozen] Mock<IMediator> mediatorMock,
          [Frozen] Mock<IConfiguration> configurationMock,
          [Frozen] Mock<ILogger<AuthService>> loggerMock,
          [Frozen] Mock<IJwtTokenHandler> jwtTokenHandlerMock,
          IFixture fixture,
          AuthService authService,
          LoginUserDto validLoginUser,
          UserDto mockUser)
    {
        // Arrange
        validLoginUser.Password = mockUser.Password;
        var token = fixture.Create<SecurityToken>();
        var userToken= fixture.Create<string>();

        mockUser.Password = Argon2.Hash(validLoginUser.Password); // Hashing the password
        configurationMock.SetupGet(c => c["JWT:SecretKey"]).Returns("test_secret_key");

        jwtTokenHandlerMock.Setup(x => x.CreateToken(It.IsAny<SecurityTokenDescriptor>()))
                         .Returns(token);
        jwtTokenHandlerMock.Setup(x => x.WriteToken(token))
                          .Returns(userToken);
        mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(mockUser);

        // Act
        var result = await authService.LoginAsync(validLoginUser);

        // Assert
        result.Should().NotBeNull();
        result.UserName.Should().Be(mockUser.UserName);
        result.Token.Should().NotBeNullOrEmpty();
        result.Token.Should().Be(userToken);
    }


    [Theory, AutoMoqData]
    public async Task LoginAsync_InvalidUser_ReturnsNull(
        [Frozen] Mock<IMediator> mediatorMock,
        [Frozen] Mock<IConfiguration> configurationMock,
        [Frozen] Mock<ILogger<AuthService>> loggerMock,
        AuthService authService,
        LoginUserDto invalidLoginUser)
    {
        // Arrange
        mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((UserDto)null);

        // Act
        var result = await authService.LoginAsync(invalidLoginUser);

        // Assert
        result.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public async Task RegisterAsync_ValidUser_Succeeds(
        [Frozen] Mock<IMediator> mediatorMock,
        [Frozen] Mock<ILogger<AuthService>> loggerMock,
        AuthService authService,
        RegisterUserDto validRegisterUser)
    {
        // Arrange
        // Assuming no exceptions are thrown
        mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

        // Act
        Func<Task> act = async () => await authService.RegisterAsync(validRegisterUser);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Theory, AutoMoqData]
    public async Task RegisterAsync_WhenCalled_ExceptionThrown(
        [Frozen] Mock<IMediator> mediatorMock,
        [Frozen] Mock<ILogger<AuthService>> loggerMock,
        AuthService authService,
        RegisterUserDto invalidRegisterUser)
    {
        // Arrange
        mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await authService.RegisterAsync(invalidRegisterUser);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}