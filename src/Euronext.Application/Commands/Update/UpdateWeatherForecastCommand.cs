using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Commands.Update;

public sealed class UpdateWeatherForecastCommand : IRequest
{
    public WeatherForecastDto WeatherForecast { get; }
    public int Id { get; }

    public UpdateWeatherForecastCommand(int id, WeatherForecastDto weatherForecast)
    {
        WeatherForecast = weatherForecast;
        Id = id;
    }
}
