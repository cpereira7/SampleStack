namespace SampleStack.Repository.Repositories.Tests
{
    public class MemoryRepositoryTests : RepositoryTests
    {
        public MemoryRepositoryTests() : base(new MemoryRepository<TestRecord>())
        {
        }

    }
}