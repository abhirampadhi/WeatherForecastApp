﻿using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Queries;

public sealed class GetWeatherForecastByIdQuery: IRequest<WeatherForecastDto>
{
    public int Id { get; }
    public GetWeatherForecastByIdQuery(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        ArgumentOutOfRangeException.ThrowIfZero(id);

        Id = id;
    }
}
