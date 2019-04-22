using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Queries;
using FullStack.Database;
using FullStack.Database.Models;

namespace FullStack.Functions.Queries
{
    public class CurrentCountQueryHandler : IQueryHandler<CurrentCountQuery, int>
    {
        private readonly ICloudTableRepository<Count> _countRepository;

        public CurrentCountQueryHandler(ICloudTableRepository<Count> countRepository)
        {
            _countRepository = countRepository;
        }

        public async Task<int> HandleAsync(CurrentCountQuery query)
        {
            var result = await _countRepository.RetrieveAsync(Count.Default);

            return result?.Value ?? 0;
        }
    }
}