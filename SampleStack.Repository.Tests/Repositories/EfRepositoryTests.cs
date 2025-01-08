using Microsoft.EntityFrameworkCore;

namespace SampleStack.Repository.Repositories.Tests
{
    public class EfRepositoryTests : RepositoryTests
    {
        public EfRepositoryTests() : base(new EfRepository<TestRecord>(CreateInMemoryDbContext()))
        {
        }

        private static DbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new TestDbContext(options);
        }

        private class TestDbContext : DbContext
        {
            public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

            public DbSet<TestRecord> TestRecords { get; set; }
        }
    }
}