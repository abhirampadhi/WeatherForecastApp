using Euronext.WeatherForecastAppDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Euronext.WeatherForecastApp.Infrastructure.Data;
public class EuronextDbContext : DbContext
{
    public EuronextDbContext(DbContextOptions<EuronextDbContext> options)
        : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}