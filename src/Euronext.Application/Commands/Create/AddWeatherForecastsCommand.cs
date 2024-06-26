using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Commands.Create;

public sealed class AddWeatherForecastsCommand : IRequest
{
    public List<WeatherForecastDto> WeatherForecasts { get; }

    public AddWeatherForecastsCommand(List<WeatherForecastDto> weatherForecasts)
    {
        WeatherForecasts = weatherForecasts;
    }
}