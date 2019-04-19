using System.Threading.Tasks;
using CommandQuery.AzureFunctions;
using CommandQuery.DependencyInjection;
using FullStack.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FullStack.Functions
{
    public static class Query
    {
        private static readonly QueryFunction Func = new QueryFunction(new[] { typeof(Query).Assembly, typeof(WeatherForecastQuery).Assembly }.GetQueryProcessor());

        [FunctionName("Query")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "query/{queryName}")] HttpRequest req, ILogger log, string queryName)
        {
            return await Func.Handle(queryName, req, log);
        }
    }
}