using Euronext.WeatherForecastApp.Infrastructure.Data;
using Euronext.WeatherForecastAppDomain.Entities;
using Euronext.WeatherForecastAppDomain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Euronext.WeatherForecastApp.Infrastructure.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly EuronextDbContext _context;
    private readonly ILogger<WeatherForecastRepository> _logger;

    public WeatherForecastRepository(EuronextDbContext context, ILogger<WeatherForecastRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WeatherForecast> GetByIdAsync(int id)
    {
        try
        {
            return await _context.WeatherForecasts.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting forecast by ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        try
        {
            return await _context.WeatherForecasts.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all forecasts.");
            throw;
        }
    }
    public async Task<IEnumerable<WeatherForecast>> GetByDateAsync(DateTime date)
    {
        try
        {
            return await _context.WeatherForecasts
                .Where(f => f.Date.Date == date.Date)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting forecasts by date: {Date}", date);
            throw;
        }
    }

    public async Task AddAsync(List<WeatherForecast> forecasts)
    {
        try
        {
            await _context.WeatherForecasts.AddRangeAsync(forecasts);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding multiple forecasts.");
            throw;
        }
    }

    public async Task UpdateAsync(WeatherForecast forecast)
    {
        try
        {
            _context.WeatherForecasts.Update(forecast);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a forecast.");
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var forecast = await _context.WeatherForecasts.FindAsync(id);
            if (forecast != null)
            {
                _context.WeatherForecasts.Remove(forecast);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Forecast with ID: {Id} was not found.", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a forecast by ID: {Id}", id);
            throw;
        }
    }
}
