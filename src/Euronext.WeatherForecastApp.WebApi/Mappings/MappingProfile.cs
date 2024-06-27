using AutoMapper;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Models.Responses;
using Euronext.WeatherForecastApp.Domain.Entities;
using Euronext.WeatherForecastAppDomain.Entities;

namespace Euronext.WeatherForecastAppWebApi.Mappings;

/// <summary>
/// A profile class for configuring object-object mappings using AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WeatherForecastDto, WeatherForecast>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap().ReverseMap();
        CreateMap<WeatherForecastRequestModel, WeatherForecastDto>().ReverseMap();
        CreateMap<WeatherForecastResponseModel, WeatherForecastDto>().ReverseMap();
    }
}
