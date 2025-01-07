using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.Repository;
using SampleStack.Repository.Repositories;
using SampleStack.Repository.Services;

// See https://aka.ms/new-console-template for more information

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) => { config.AddEnvironmentVariables(); })
    .ConfigureServices((context, services) =>
    {

        if (context.HostingEnvironment.IsDevelopment())
        {
            services.AddSingleton(typeof(IRepository<>), typeof(MemoryRepository<>));
        }
        else
        {
            services.AddSingleton(typeof(IRepository<>), typeof(EfRepository<>));
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

            Console.WriteLine($"Using connection string: {connectionString}");

            services.AddDbContext<DbContext, ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }

        services.AddSingleton<IPersonService, PersonService>();
    })
    .Build();

using var scope = host.Services.CreateScope();

var hostingEnvironment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
if (!hostingEnvironment.IsDevelopment())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    Console.WriteLine("Database migrated");
}

var personService = scope.ServiceProvider.GetRequiredService<IPersonService>();

PrintPersonList(personService);

personService.UpdatePerson(1, "John", "Smith");

PrintPersonList(personService);

personService.RemovePerson(1);

PrintPersonList(personService);

static void PrintPersonList(IPersonService personService)
{
    Console.WriteLine("-------");

    foreach (var person in personService.GetPersons())
    {
        Console.WriteLine(person);
    }
}

