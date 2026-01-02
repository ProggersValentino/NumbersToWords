using Microsoft.EntityFrameworkCore;
using NumbersToWords.WebMinRouteGroup.data;


namespace NumberToWordsTests.UnitTestHelpers
{
    internal class MockDb : IDbContextFactory<NumToWordDb>
    {
        public NumToWordDb CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<NumToWordDb>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new NumToWordDb( options );
        }

        
    }
}
