using Euronext.WeatherForecastApp.Domain.Interface;
using Euronext.WeatherForecastApp.Infrastructure.Data;
using Euronext.WeatherForecastApp.Infrastructure.Repositories;
using Euronext.WeatherForecastAppDomain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Euronext.WeatherForecastApp.Infrastructure;

public static class DependencyInjections
{
    public static void AddInfrastructureConfigureServices(this IServiceCollection services)
    {
        // Register EuronextDbContext with in-memory database for assignment
        services.AddDbContext<EuronextDbContext>(options =>
            options.UseInMemoryDatabase("WeatherForecastDB"));

        // Register UserDbContext with in-memory database for assignment
        services.AddDbContext<UserDbContext>(options =>
            options.UseInMemoryDatabase("UserDB"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
    }
}