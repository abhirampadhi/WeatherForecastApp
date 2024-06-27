using AutoFixture;
using AutoFixture.AutoMoq;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Validators;
using FluentValidation.TestHelper;

namespace Euronext.WeatherForecastApp.Application.Tests.Validators;

public class WeatherForecastRequestModelValidatorTests
{
    private readonly IFixture _fixture;

    public WeatherForecastRequestModelValidatorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [InlineData("0001-01-01", 20, "Forecast date cannot be in the past.")]
    public void Validate_InvalidModel_ReturnsValidationError(string dateStr, int temperatureC, string expectedErrorMessage)
    {
        // Arrange
        var validator = _fixture.Create<WeatherForecastRequestModelValidator>();
        var date = DateTime.Parse(dateStr);
        var model = new WeatherForecastRequestModel { Date = date, TemperatureC = temperatureC };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Date)
        .WithErrorMessage(expectedErrorMessage);

    }

    [Theory]
    [InlineData("2030-12-01", 25)]
    [InlineData("2030-06-01", 0)]
    [InlineData("2030-06-01", 60)]
    public void Validate_ValidModel_NoValidationErrors(string dateStr, int temperatureC)
    {
        // Arrange
        var validator = _fixture.Create<WeatherForecastRequestModelValidator>();
        var date = DateTime.Parse(dateStr);
        var model = new WeatherForecastRequestModel { Date = date, TemperatureC = temperatureC };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}