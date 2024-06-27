using Isopoh.Cryptography.Argon2;

namespace Euronext.WeatherForecastApp.Common.Tests.Password;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        return Argon2.Hash(password);
    }
}


