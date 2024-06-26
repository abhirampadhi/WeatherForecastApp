namespace Euronext.WeatherForecastApp.Application.Models.Requests;

public class WeatherForecastRequestModel
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
