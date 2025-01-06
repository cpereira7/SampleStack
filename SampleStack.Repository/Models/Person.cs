namespace SampleStack.Repository.Models
{
    internal record Person : IModelBase
    {
        public int Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
    }
}
