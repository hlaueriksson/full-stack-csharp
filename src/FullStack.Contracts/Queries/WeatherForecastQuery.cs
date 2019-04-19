using System;
using CommandQuery;

namespace FullStack.Contracts.Queries
{
    public class WeatherForecastQuery : IQuery<WeatherForecast[]>
    {
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }
    }
}