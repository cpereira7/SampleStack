using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.AutoMapper.Configuration;
using SampleStack.AutoMapper.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationServices();
    })
    .Build();

var serviceProvider = host.Services;

var dataRetrievalFacade = serviceProvider.GetRequiredService<DataRetrievalFacade>();

await dataRetrievalFacade.RetrieveAllDataAsync();

var orders = dataRetrievalFacade.GetAllOrders();

Console.WriteLine("Orders List!");

foreach (var order in orders)
{
    Console.WriteLine($"Order ID: {order.Id}");
    Console.WriteLine($"Customer: {order.Customer?.Name}");
    foreach (var item in order.Items)
    {
        Console.WriteLine($"  Product: {item.Product?.Name}, Quantity: {item.Quantity}");
    }
    Console.WriteLine($"Total: { order.TotalAmount:N2}");
}
