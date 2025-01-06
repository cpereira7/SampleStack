using SampleStack.Repository.Models;
using SampleStack.Repository.Repositories;

namespace SampleStack.Repository.Services
{
    internal class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personsRepository;

        public PersonService(IRepository<Person> personsRepository)
        {
            _personsRepository = personsRepository;

            BootStrap();
        }

        private void BootStrap()
        {
            _personsRepository.Add(new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 5, 15)
            });

            _personsRepository.Add(new Person
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 8, 22)
            });
        }

        public IReadOnlyCollection<Person> GetPersons()
        {
            return _personsRepository.GetAll();
        }
    }
}
