using MediatR;

namespace Euronext.WeatherForecastApp.Application.Commands.Delete;

public sealed class DeleteWeatherForecastCommand : IRequest
{
    public int Id { get; }

    public DeleteWeatherForecastCommand(int id)
    {
        Id = id;
    }
}