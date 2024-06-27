using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Euronext.WeatherForecastApp.Common.JwtWrapper;

public class JwtTokenHandlerWrapper : IJwtTokenHandler
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtTokenHandlerWrapper()
    {
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public string WriteToken(SecurityToken token)
    {
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    public SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        return _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
    }
}
