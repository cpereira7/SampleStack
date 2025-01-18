using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Profiles;
using SampleStack.AutoMapper.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Add DataSources
        services.AddScoped<IDataSource<CustomerDto>, CustomerDataSource>();
        services.AddScoped<IDataSource<OrderDto>, OrderDataSource>();
        services.AddScoped<IDataSource<ProductDto>, ProductDataSource>();

        // Add Services
        services.AddSingleton<OrderService>();
        services.AddSingleton<ProductService>();
        services.AddSingleton<CustomerService>();
        services.AddSingleton<DataRetrievalFacade>();
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
}
