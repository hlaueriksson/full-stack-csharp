using System.Threading.Tasks;
using CommandQuery.AzureFunctions;
using CommandQuery.DependencyInjection;
using FullStack.Contracts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FullStack.Functions
{
    public static class Command
    {
        private static readonly CommandFunction Func = new CommandFunction(new[] { typeof(Command).Assembly, typeof(IncrementCountCommand).Assembly }.GetCommandProcessor());

        [FunctionName("Command")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "command/{commandName}")] HttpRequest req, ILogger log, string commandName)
        {
            return await Func.Handle(commandName, req, log);
        }
    }
}