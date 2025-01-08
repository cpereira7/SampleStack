using SampleStack.Repository.Models;

namespace SampleStack.Repository.Repositories.Tests
{
    public record TestRecord : IModelBase
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
