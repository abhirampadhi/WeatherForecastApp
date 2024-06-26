using Euronext.WeatherForecastApp.Application.Interfaces;
using Euronext.WeatherForecastApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Euronext.WeatherForecastApp.Application;

public static class DependencyInjections
{
    public static void AddApplicationConfigureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IAuthService, AuthService>();
    }
}