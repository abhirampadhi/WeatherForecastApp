using Euronext.WeatherForecastApp.Common.JwtWrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Euronext.WeatherForecastApp.Common;

public static class DependencyInjections
{
    public static void AddCommonConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenHandler, JwtTokenHandlerWrapper>();
    }
}