namespace Euronext.WeatherForecastApp.Common.Utilities;

public static class TemperatureCDescriptor
{
    public static string GetDescription(int TemperatureC)
    {
        return TemperatureC switch
        {
            <= -10 => "Freezing",
            <= 0 => "Bracing",
            <= 10 => "Chilly",
            <= 15 => "Cool",
            <= 20 => "Mild",
            <= 25 => "Warm",
            <= 30 => "Balmy",
            <= 35 => "Hot",
            <= 40 => "Sweltering",
            _ => "Scorching"
        };
    }
}