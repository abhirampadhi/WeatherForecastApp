using AutoFixture;
using Euronext.WeatherForecastApp.Application.Queries;
using FluentAssertions;

namespace Euronext.WeatherForecastApp.Application.Tests.Queries;

public class GetWeatherForecastByIdQueryTests
{
    private readonly IFixture _fixture;

    public GetWeatherForecastByIdQueryTests()
    {
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void Constructor_Id_SetCorrectly(int id)
    {
        // Arrange & Act
        var query = new GetWeatherForecastByIdQuery(id);

        // Assert
        query.Id.Should().Be(id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_InvalidId_ThrowsArgumentException(int id)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new GetWeatherForecastByIdQuery(id));
    }
}