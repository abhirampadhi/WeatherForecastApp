using Euronext.WeatherForecastApp.Domain.Entities;

namespace Euronext.WeatherForecastApp.Domain.Interface;

public interface IUserRepository
{
    Task<User> GetByUserNameAsync(string userName);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddUserAsync(User user);
}
