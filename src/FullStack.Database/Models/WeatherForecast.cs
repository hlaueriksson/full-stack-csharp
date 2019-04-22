using System;
using Microsoft.Azure.Cosmos.Table;

namespace FullStack.Database.Models
{
    public class WeatherForecast : TableEntity
    {
        public string Location { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }

        public WeatherForecast()
        {
        }

        public WeatherForecast(string location, DateTime date) : base(location, date.ToShortDateString())
        {
            Location = location;
            Date = date;
        }
    }
}