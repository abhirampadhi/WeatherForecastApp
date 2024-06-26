using Euronext.WeatherForecastApp.Application.Models.Requests;
using FluentValidation;

namespace Euronext.WeatherForecastApp.Application.Validators;

public sealed class WeatherForecastRequestModelValidator : AbstractValidator<WeatherForecastRequestModel>
{
    public WeatherForecastRequestModelValidator()
    {
        RuleFor(x => x.Date)
            .GreaterThan(DateTime.Now)
            .WithMessage("Forecast date cannot be in the past.");

        RuleFor(x => x.TemperatureC)
            .InclusiveBetween(-60, 60)
            .WithMessage("TemperatureC must be between -60 and 60 degrees.");
    }
}