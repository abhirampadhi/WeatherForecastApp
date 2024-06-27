using AutoFixture;
using Euronext.WeatherForecastApp.Application.Queries;
using FluentAssertions;

namespace Euronext.WeatherForecastApp.Application.Tests.Queries;
public class GetWeatherForecastsByDateQueryTests
{
    private readonly IFixture _fixture;

    public GetWeatherForecastsByDateQueryTests()
    {
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("2023-01-01")]
    [InlineData("2023-06-15")]
    public void Constructor_Date_SetCorrectly(string dateStr)
    {
        // Arrange
        var date = DateTime.Parse(dateStr);

        // Act
        var query = new GetWeatherForecastsByDateQuery(date);

        // Assert
        query.Date.Should().Be(date);
    }

    [Fact]
    public void Constructor_NullDate_ThrowsArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new GetWeatherForecastsByDateQuery(default));
    }
}
