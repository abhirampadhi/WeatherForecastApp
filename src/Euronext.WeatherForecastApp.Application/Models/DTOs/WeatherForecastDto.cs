namespace Euronext.WeatherForecastApp.Application.Models.DTOs;
public sealed class WeatherForecastDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }

    public string TemperatureCDescription { get; set; }
}

