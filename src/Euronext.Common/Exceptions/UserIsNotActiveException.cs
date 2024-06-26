namespace Euronext.WeatherForecastAppCommon.Exceptions;
public class UserIsNotActiveException : Exception
{
    public UserIsNotActiveException() : base("User is not active")
    { }
}
