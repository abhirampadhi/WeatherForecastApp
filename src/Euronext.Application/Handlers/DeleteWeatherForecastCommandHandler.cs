using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Delete;
using Euronext.WeatherForecastAppCommon.Exceptions;
using Euronext.WeatherForecastAppDomain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class DeleteWeatherForecastCommandHandler : IRequestHandler<DeleteWeatherForecastCommand>
{
    private readonly IWeatherForecastRepository _repository;

    public DeleteWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(request));

        var forecastToDelete = await _repository.GetByIdAsync(request.Id);

        if (forecastToDelete == null)
        {
            throw new NotFoundException($"Weather forecast with ID {request.Id} not found.");
        }

        await _repository.DeleteAsync(forecastToDelete.Id);
    }
}