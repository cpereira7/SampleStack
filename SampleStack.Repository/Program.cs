using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.Repository.Repositories;
using SampleStack.Repository.Services;

// See https://aka.ms/new-console-template for more information

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(typeof(IRepository<>), typeof(MemoryRepository<>));
        services.AddSingleton<IPersonService, PersonService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var personService = scope.ServiceProvider.GetRequiredService<IPersonService>();

foreach (var person in personService.GetPersons())
{
    Console.WriteLine(person);
}
