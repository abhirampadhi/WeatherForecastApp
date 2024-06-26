using Euronext.WeatherForecastApp.Application.Models.DTOs;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Queries;

public sealed class GetUserQuery : IRequest<UserDto>
{
    public string UserName { get; set; }
    public GetUserQuery(string userName)
    {
        UserName = userName;
    }
}
