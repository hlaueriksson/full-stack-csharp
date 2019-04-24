using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FullStack.Contracts.Queries;
using FullStack.Database;
using FullStack.Functions.Queries;
using NSubstitute;
using NUnit.Framework;
using WeatherForecast = FullStack.Database.Models.WeatherForecast;

namespace FullStack.Tests.Functions
{
    public class WeatherForecastQueryHandlerTests
    {
        [Test]
        public async Task Should_return_all_weather_forecasts()
        {
            var expected = new WeatherForecast { Date = DateTime.Today, TemperatureC = 1, TemperatureF = 33, Summary = "Freezing" };

            var weatherForecastRepository = Substitute.For<IWeatherForecastRepository>();
            weatherForecastRepository.All().Returns(new[] { expected });
            var subject = new WeatherForecastQueryHandler(weatherForecastRepository);

            var result = await subject.HandleAsync(new WeatherForecastQuery());
            var single = result.Single();

            single.Date.Should().Be(expected.Date);
            single.TemperatureC.Should().Be(expected.TemperatureC);
            single.TemperatureF.Should().Be(expected.TemperatureF);
            single.Summary.Should().Be(expected.Summary);
        }
    }
}