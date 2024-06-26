using System.ComponentModel.DataAnnotations;

namespace Euronext.WeatherForecastApp.Application.Models.DTOs;

public class UserDto
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public List<string>? Roles { get; set; }
    public bool IsActive { get; set; }
    public string? Token { get; set; }
    public string Password { get; set; }

}
