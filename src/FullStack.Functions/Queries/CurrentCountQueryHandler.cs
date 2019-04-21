using System.Threading.Tasks;
using CommandQuery;
using FullStack.Contracts.Queries;

namespace FullStack.Functions.Queries
{
    public class CurrentCountQueryHandler : IQueryHandler<CurrentCountQuery, int>
    {
        public static int Count = 0;

        public Task<int> HandleAsync(CurrentCountQuery query)
        {
            return Task.FromResult(Count);
        }
    }
}