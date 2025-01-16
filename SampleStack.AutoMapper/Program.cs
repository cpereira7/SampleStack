// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.AutoMapper;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.DTOs;

Console.WriteLine("Hello, World!");

// Setup dependency injection
var services = new ServiceCollection();

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

var serviceProvider = services.BuildServiceProvider();

// Fetch and display orders
var orderService = serviceProvider.GetRequiredService<OrderService>();
var productService = serviceProvider.GetRequiredService<ProductService>();
var customerService = serviceProvider.GetRequiredService<CustomerService>();

await productService.RetrieveData();
await customerService.RetrieveData();
await orderService.RetrieveData();

var orders = orderService.GetAllItems();

foreach (var order in orders)
{
    Console.WriteLine($"Order ID: {order.Id}");
    Console.WriteLine($"Customer: {order.Customer?.Name}");
    foreach (var item in order.Items)
    {
        Console.WriteLine($"  Product: {item.Product?.Name}, Quantity: {item.Quantity}");
    }
}

