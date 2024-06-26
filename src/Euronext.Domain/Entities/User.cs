using System.ComponentModel.DataAnnotations;

namespace Euronext.WeatherForecastApp.Domain.Entities;

public sealed class User
{
    [Key]
    public string UserName { get; set; }
    public string Name { get; set; }
    public List<string>? Roles { get; set; }
    public bool IsActive { get; set; }
    public string? Token { get; set; }
    public string Password { get; set; }
}
