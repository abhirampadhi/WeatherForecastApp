using Microsoft.IdentityModel.Tokens;

namespace Euronext.WeatherForecastApp.Common.JwtWrapper;
public interface IJwtTokenHandler
{
    string WriteToken(SecurityToken token);
    SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);
}
