using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Queries;

public sealed class GetWeatherForecastsByDateQuery : IRequest<IEnumerable<WeatherForecastDto>>
{
    public DateTime Date { get; }
    public GetWeatherForecastsByDateQuery(DateTime date)
    {
        if (date == DateTime.MinValue)
        {
            throw new ArgumentException("Date cannot be default");
        }

        Date = date;
    }
}
