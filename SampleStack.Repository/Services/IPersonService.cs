using SampleStack.Repository.Models;

namespace SampleStack.Repository.Services
{
    internal interface IPersonService
    {
        IReadOnlyCollection<Person> GetPersons();
    }
}
