using AutoFixture;
using AutoFixture.AutoMoq;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Validators;
using FluentValidation.TestHelper;

namespace Euronext.WeatherForecastApp.Application.Tests.Validators;

public class LoginUserModelRequestValidatorTests
{
    private readonly IFixture _fixture;

    public LoginUserModelRequestValidatorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [InlineData(null, "password", "User name must be entered.")]
    [InlineData("", "password", "User name must be entered.")]
    [InlineData("us", "password", "User name must be at least 3 characters long.")]
    public void Validate_InvalidUserName_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<LoginUserModelRequestValidator>();
        var model = new LoginUserModelRequest { UserName = userName, Password = password };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("user", null, "Password must be entered.")]
    [InlineData("user", "", "Password must be entered.")]
    [InlineData("user", "TTT", "Password must be at least 4 characters long.")]
    public void Validate_InvalidPassword_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<LoginUserModelRequestValidator>();
        var model = new LoginUserModelRequest { UserName = userName, Password = password };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("user", "password")]
    [InlineData("username", "securepassword")]
    public void Validate_ValidModel_NoValidationErrors(string userName, string password)
    {
        // Arrange
        var validator = _fixture.Create<LoginUserModelRequestValidator>();
        var model = new LoginUserModelRequest { UserName = userName, Password = password };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
