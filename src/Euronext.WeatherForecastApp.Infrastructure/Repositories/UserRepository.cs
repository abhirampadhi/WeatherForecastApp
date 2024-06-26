using Euronext.WeatherForecastApp.Domain.Entities;
using Euronext.WeatherForecastApp.Domain.Interface;
using Euronext.WeatherForecastApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Euronext.WeatherForecastApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByUserNameAsync(string userName)
    {
        return await _context.Users.FindAsync(userName);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
