namespace Euronext.WeatherForecastApp.Application.Models.Responses;

public sealed class WeatherForecastResponseModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
