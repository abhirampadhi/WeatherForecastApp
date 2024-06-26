using Euronext.WeatherForecastApp.Application.Models.DTOs;

namespace Euronext.WeatherForecastApp.Application.Interfaces;

public interface IAuthService
{
    Task<UserDto> LoginAsync(LoginUserDto loginUser);
    Task RegisterAsync(RegisterUserDto registerUser);
}
