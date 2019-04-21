using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Commands;
using FullStack.Functions.Queries;

namespace FullStack.Functions.Commands
{
    public class IncrementCountCommandHandler : ICommandHandler<IncrementCountCommand>
    {
        public Task HandleAsync(IncrementCountCommand command)
        {
            CurrentCountQueryHandler.Count++;

            return Task.CompletedTask;
        }
    }
}