using AutoFixture;
using AutoFixture.AutoMoq;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Validators;
using FluentValidation.TestHelper;

namespace Euronext.WeatherForecastApp.Application.Tests.Validators;

public class RegisterUserModelRequestValidatorTests
{
    private readonly IFixture _fixture;

    public RegisterUserModelRequestValidatorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [InlineData(null, "username", "password", new string[] { "User" }, "Name must be entered.")]
    [InlineData("", "username", "password", new string[] { "User" }, "Name must be entered.")]
    [InlineData("a", "username", "password", new string[] { "User" }, "Name must be at least 2 characters long.")]
    public void Validate_InvalidName_ReturnsValidationError(string name, string userName, string password, string[] roles, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<RegisterUserModelRequestValidator>();
        var model = new RegisterUserModelRequest { Name = name, UserName = userName, Password = password, Roles = roles.ToList() };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Name", null, "password", new string[] { "User" }, "User name must be entered.")]
    [InlineData("Name", "", "password", new string[] { "User" }, "User name must be entered.")]
    [InlineData("Name", "us", "password", new string[] { "User" }, "User name must be at least 3 characters long.")]
    public void Validate_InvalidUserName_ReturnsValidationError(string name, string userName, string password, string[] roles, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<RegisterUserModelRequestValidator>();
        var model = new RegisterUserModelRequest { Name = name, UserName = userName, Password = password, Roles = roles.ToList() };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Name", "username", null, new string[] { "User" }, "Password must be entered.")]
    [InlineData("Name", "username", "", new string[] { "User" }, "Password must be entered.")]
    [InlineData("Name", "username", "TTT", new string[] { "User" }, "Password must be at least 4 characters long.")]
    public void Validate_InvalidPassword_ReturnsValidationError(string name, string userName, string password, string[] roles, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<RegisterUserModelRequestValidator>();
        var model = new RegisterUserModelRequest { Name = name, UserName = userName, Password = password, Roles = roles.ToList() };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Name", "username", "password", new string[] { }, "Roles cannot be empty if provided.")]
    [InlineData("Name", "username", "password", new string[] { "InvalidRole" }, "Only 'User' and 'Admin' roles are allowed.")]
    public void Validate_InvalidRoles_ReturnsValidationError(string name, string userName, string password, string[] roles, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<RegisterUserModelRequestValidator>();
        var model = new RegisterUserModelRequest { Name = name, UserName = userName, Password = password, Roles = roles.ToList() };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Roles)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Name", "username", "password", new string[] { "User" })]
    [InlineData("Name", "username", "password", new string[] { "Admin" })]
    [InlineData("Name", "username", "password", new string[] { "User", "Admin" })]
    public void Validate_ValidModel_NoValidationErrors(string name, string userName, string password, string[] roles)
    {
        // Arrange
        var validator = _fixture.Create<RegisterUserModelRequestValidator>();
        var model = new RegisterUserModelRequest { Name = name, UserName = userName, Password = password, Roles = roles.ToList() };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
