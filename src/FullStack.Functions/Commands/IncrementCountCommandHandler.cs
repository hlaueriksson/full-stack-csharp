using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Commands;
using FullStack.Database.AzureTableStorage;
using FullStack.Database.AzureTableStorage.Models;

namespace FullStack.Functions.Commands
{
    public class IncrementCountCommandHandler : ICommandHandler<IncrementCountCommand>
    {
        private readonly ICloudTableRepository<Count> _countRepository;

        public IncrementCountCommandHandler(ICloudTableRepository<Count> countRepository)
        {
            _countRepository = countRepository;
        }

        public async Task HandleAsync(IncrementCountCommand command)
        {
            var result = await _countRepository.RetrieveAsync(Count.Default) ?? Count.Default;
            result.Value++;
            await _countRepository.InsertOrMergeAsync(result);
        }
    }
}