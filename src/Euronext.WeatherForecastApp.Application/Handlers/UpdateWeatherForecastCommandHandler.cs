using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Update;
using Euronext.WeatherForecastAppCommon.Exceptions;
using Euronext.WeatherForecastAppDomain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class UpdateWeatherForecastCommandHandler : IRequestHandler<UpdateWeatherForecastCommand>
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IMapper _mapper;

    public UpdateWeatherForecastCommandHandler(IWeatherForecastRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(request));

        var existingForecast = await _repository.GetByIdAsync(request.Id);

        if (existingForecast == null)
        {
            throw new NotFoundException($"Weather forecast with ID {request.Id} not found.");
        }

        existingForecast.Date = request.WeatherForecast.Date;
        existingForecast.TemperatureC = request.WeatherForecast.TemperatureC;
        existingForecast.Summary = request.WeatherForecast.Summary;

        await _repository.UpdateAsync(existingForecast);
    }
}