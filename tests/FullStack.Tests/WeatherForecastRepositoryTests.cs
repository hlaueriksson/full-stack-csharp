using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FullStack.Database;
using FullStack.Database.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FullStack.Tests
{
    public class WeatherForecastRepositoryTests
    {
        [Test, Explicit]
        public async Task Populate_sample_data()
        {
            var connectionString = "CHANGEME";
            var subject = new WeatherForecastRepository(connectionString);

            var data = await GetSampleData();

            foreach (var weatherForecast in data)
            {
                weatherForecast.PartitionKey = "Stockholm";
                weatherForecast.RowKey = weatherForecast.Date.ToShortDateString();

                await subject.InsertAsync(weatherForecast);
            }
        }

        private async Task<WeatherForecast[]> GetSampleData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(x => x.EndsWith("weather.json"));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<WeatherForecast[]>(json);
            }
        }
    }
}