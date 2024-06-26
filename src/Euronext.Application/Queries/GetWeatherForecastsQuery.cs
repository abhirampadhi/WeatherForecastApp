using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Queries;

public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDto>>
{

}