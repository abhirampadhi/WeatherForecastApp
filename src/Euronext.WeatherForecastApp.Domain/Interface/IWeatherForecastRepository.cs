using Euronext.WeatherForecastAppDomain.Entities;

namespace Euronext.WeatherForecastAppDomain.Interface;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast> GetByIdAsync(int id);
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<IEnumerable<WeatherForecast>> GetByDateAsync(DateTime date);
    Task AddAsync(List<WeatherForecast> forecasts);
    Task UpdateAsync(WeatherForecast forecast);
    Task DeleteAsync(int id);
}
