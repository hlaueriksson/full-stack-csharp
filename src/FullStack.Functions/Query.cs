using System;
using System.Threading.Tasks;
using CommandQuery.AzureFunctions;
using CommandQuery.DependencyInjection;
using FullStack.Contracts.Queries;
using FullStack.Database;
using FullStack.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullStack.Functions
{
    public static class Query
    {
        private static readonly QueryFunction Func = new QueryFunction(new[] { typeof(Query).Assembly, typeof(WeatherForecastQuery).Assembly }.GetQueryProcessor(GetServiceCollection()));

        [FunctionName("Query")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "query/{queryName}")] HttpRequest req, ILogger log, string queryName)
        {
            return await Func.Handle(queryName, req, log);
        }

        private static IServiceCollection GetServiceCollection()
        {
            var connectionString = GetEnvironmentVariable("ConnectionStrings:Cosmos");

            var services = new ServiceCollection();
            // Add handler dependencies
            services.AddSingleton<ICloudTableRepository<Count>>(provider => new CloudTableRepository<Count>(connectionString));
            services.AddSingleton<IWeatherForecastRepository>(provider => new WeatherForecastRepository(connectionString));

            return services;
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}