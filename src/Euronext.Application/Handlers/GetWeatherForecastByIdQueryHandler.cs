using AutoMapper;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastApp.Common.Utilities;
using Euronext.WeatherForecastAppDomain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class GetWeatherForecastByIdQueryHandler : IRequestHandler<GetWeatherForecastByIdQuery, WeatherForecastDto>
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IMapper _mapper;

    public GetWeatherForecastByIdQueryHandler(IWeatherForecastRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        var forecast = await _repository.GetByIdAsync(request.Id);
        return  new WeatherForecastDto
        {
            Id = request.Id,    
            Date = forecast.Date,
            TemperatureC = forecast.TemperatureC,
            Summary = forecast.Summary,
            TemperatureCDescription = TemperatureCDescriptor.GetDescription(forecast.TemperatureC)
        };
    }
}
