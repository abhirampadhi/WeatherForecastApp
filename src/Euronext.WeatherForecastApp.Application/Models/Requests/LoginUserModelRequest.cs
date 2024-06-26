namespace Euronext.WeatherForecastApp.Application.Models.Requests;

public sealed class LoginUserModelRequest
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
