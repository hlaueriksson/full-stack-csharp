using System.Collections.Generic;
using FullStack.Database.Models;
using Microsoft.Azure.Cosmos.Table;

namespace FullStack.Database
{
    public interface IWeatherForecastRepository : ICloudTableRepository<WeatherForecast>
    {
        IEnumerable<WeatherForecast> All();
    }

    public class WeatherForecastRepository : CloudTableRepository<WeatherForecast>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<WeatherForecast> All()
        {
            var query = new TableQuery<WeatherForecast>();

            return Table.ExecuteQuery(query);
        }
    }
}