using System.Linq;
using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Queries;
using FullStack.Database;

namespace FullStack.Functions.Queries
{
    public class WeatherForecastQueryHandler : IQueryHandler<WeatherForecastQuery, WeatherForecast[]>
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastQueryHandler(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<WeatherForecast[]> HandleAsync(WeatherForecastQuery query)
        {
            var result = _weatherForecastRepository.All();

            return result.Select(x => new WeatherForecast
            {
                Date = x.Date,
                TemperatureC = x.TemperatureC,
                TemperatureF = x.TemperatureF,
                Summary = x.Summary
            }).ToArray();
        }
    }
}