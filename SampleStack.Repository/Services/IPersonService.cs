using SampleStack.Repository.Models;

namespace SampleStack.Repository.Services
{
    internal interface IPersonService
    {
        IReadOnlyCollection<Person> GetPersons();

        void UpdatePerson(int id, string name, string lastName);
        void RemovePerson(int id);
    }
}
