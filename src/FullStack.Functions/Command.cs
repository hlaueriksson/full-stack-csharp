using System;
using System.Threading.Tasks;
using CommandQuery.AzureFunctions;
using CommandQuery.DependencyInjection;
using FullStack.Contracts.Commands;
using FullStack.Database;
using FullStack.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullStack.Functions
{
    public static class Command
    {
        private static readonly CommandFunction Func = new CommandFunction(new[] { typeof(Command).Assembly, typeof(IncrementCountCommand).Assembly }.GetCommandProcessor(GetServiceCollection()));

        [FunctionName("Command")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "command/{commandName}")] HttpRequest req, ILogger log, string commandName)
        {
            return await Func.Handle(commandName, req, log);
        }

        private static IServiceCollection GetServiceCollection()
        {
            var connectionString = GetEnvironmentVariable("CUSTOMCONNSTR_Cosmos");

            var services = new ServiceCollection();
            // Add handler dependencies
            services.AddSingleton<ICloudTableRepository<Count>>(provider => new CloudTableRepository<Count>(connectionString));

            return services;
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}