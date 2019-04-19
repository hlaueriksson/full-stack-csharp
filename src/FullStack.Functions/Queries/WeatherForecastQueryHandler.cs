using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Queries;
using Newtonsoft.Json;

namespace FullStack.Functions.Queries
{
    public class WeatherForecastQueryHandler : IQueryHandler<WeatherForecastQuery, WeatherForecast[]>
    {
        public async Task<WeatherForecast[]> HandleAsync(WeatherForecastQuery query)
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