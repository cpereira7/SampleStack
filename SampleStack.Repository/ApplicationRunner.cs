using SampleStack.Repository.Services;

namespace SampleStack.Repository
{
    internal class ApplicationRunner
    {
        private readonly IPersonService _personService;

        public ApplicationRunner(IPersonService personService)
        {
            _personService = personService;
        }

        public void Run()
        {
            Console.WriteLine("Program started");

            PrintPersonList();

            Console.WriteLine("Updated 'John Doe'");
            _personService.UpdatePerson(1, "John", "Smith");
            PrintPersonList();

            Console.WriteLine("Removed 'John Smith'");
            _personService.RemovePerson(1);
            PrintPersonList();

            Console.WriteLine("Program ended");
        }

        private void PrintPersonList()
        {
            foreach (var person in _personService.GetPersons().OrderBy(x => x.Id))
            {
                Console.WriteLine(person);
            }
        }
    }

}
