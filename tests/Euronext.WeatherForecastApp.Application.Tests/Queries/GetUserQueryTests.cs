using AutoFixture;
using AutoFixture.AutoMoq;
using Euronext.WeatherForecastApp.Application.Queries;
using FluentAssertions;

namespace Euronext.WeatherForecastApp.Application.Tests.Queries;
public class GetUserQueryTests
{
    private readonly IFixture _fixture;

    public GetUserQueryTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [InlineData("john_doe")] // Names for testing only :)
    [InlineData("jane_smith")]
    public void Constructor_UserName_SetCorrectly(string userName)
    {
        // Arrange & Act
        var query = new GetUserQuery(userName);

        // Assert
        query.UserName.Should().Be(userName);
    }
}