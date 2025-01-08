using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.Repository;
using SampleStack.Repository.Configuration;
using SampleStack.Repository.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationServices(context.Configuration, context.HostingEnvironment);

        services.AddScoped<ApplicationRunner>();
    })
    .Build();

using var scope = host.Services.CreateScope();

DatabaseInitializer.InitializeDataBase(host.Services);

var runner = scope.ServiceProvider.GetRequiredService<ApplicationRunner>();

runner.Run();
