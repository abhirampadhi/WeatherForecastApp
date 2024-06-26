using AutoMapper;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastApp.Common.Utilities;
using Euronext.WeatherForecastAppDomain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IMapper _mapper;

    public GetWeatherForecastsQueryHandler(IWeatherForecastRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherForecastDto>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await _repository.GetAllAsync();
        return forecasts.Select(f => new WeatherForecastDto
        {
            Id = f.Id,
            Date = f.Date,
            TemperatureC = f.TemperatureC,
            Summary = f.Summary,
            TemperatureCDescription = TemperatureCDescriptor.GetDescription(f.TemperatureC)
        }).ToList();
    }
}
