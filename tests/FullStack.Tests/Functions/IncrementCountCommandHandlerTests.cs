using System.Threading.Tasks;
using FullStack.Contracts.Commands;
using FullStack.Database;
using FullStack.Database.Models;
using FullStack.Functions.Commands;
using NSubstitute;
using NUnit.Framework;

namespace FullStack.Tests.Functions
{
    public class IncrementCountCommandHandlerTests
    {
        [Test]
        public async Task Should_increment_the_count()
        {
            var countRepository = Substitute.For<ICloudTableRepository<Count>>();
            var subject = new IncrementCountCommandHandler(countRepository);

            await subject.HandleAsync(new IncrementCountCommand());

            Received.InOrder(async () =>
            {
                await countRepository.InsertOrMergeAsync(Arg.Is<Count>(x => x.Value == 1));
            });
        }
    }
}