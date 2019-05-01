using System.Threading.Tasks;
using FluentAssertions;
using FullStack.Contracts.Queries;
using FullStack.Database.AzureTableStorage;
using FullStack.Database.AzureTableStorage.Models;
using FullStack.Functions.Queries;
using NSubstitute;
using NUnit.Framework;

namespace FullStack.Tests.Functions
{
    public class CurrentCountQueryHandlerTests
    {
        [Test]
        public async Task Should_return_the_current_count()
        {
            var countRepository = Substitute.For<ICloudTableRepository<Count>>();
            countRepository.RetrieveAsync(Count.Default).Returns(Task.FromResult(new Count { Value = 1 }));
            var subject = new CurrentCountQueryHandler(countRepository);

            var result = await subject.HandleAsync(new CurrentCountQuery());

            result.Should().Be(1);
        }
    }
}