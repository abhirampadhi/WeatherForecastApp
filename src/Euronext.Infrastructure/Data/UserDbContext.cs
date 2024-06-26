using Euronext.WeatherForecastApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Euronext.WeatherForecastApp.Infrastructure.Data;
public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // Adding support for List<string> property in User model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Roles)
            .HasConversion(
                u => string.Join(',', u),
                u => u.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
