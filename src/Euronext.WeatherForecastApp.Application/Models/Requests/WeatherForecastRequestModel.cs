namespace Euronext.WeatherForecastApp.Application.Models.Requests;

public sealed class WeatherForecastRequestModel
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
