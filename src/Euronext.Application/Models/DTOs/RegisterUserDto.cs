namespace Euronext.WeatherForecastApp.Application.Models.DTOs;

public sealed class RegisterUserDto
{
    public string Name { get; set; } 
    public string UserName { get; set; } 
    public string Password { get; set; } 
    public List<string>? Roles { get; set; }
}
