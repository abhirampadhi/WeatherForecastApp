using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastAppDomain.Entities;
using Euronext.WeatherForecastAppDomain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class AddWeatherForecastsCommandHandler : IRequestHandler<AddWeatherForecastsCommand>
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IMapper _mapper;

    public AddWeatherForecastsCommandHandler(IWeatherForecastRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(AddWeatherForecastsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(request));

        await _repository.AddAsync(_mapper.Map<List<WeatherForecast>>(request.WeatherForecasts));
    }
}