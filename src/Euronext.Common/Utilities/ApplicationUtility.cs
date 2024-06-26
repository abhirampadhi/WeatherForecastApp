namespace Euronext.WeatherForecastAppCommon.Utilities;
public class ApplicationUtility
{
    public static string RemoveSpaces(string input)
    {
        return input.Replace(" ", string.Empty);
    }
}
