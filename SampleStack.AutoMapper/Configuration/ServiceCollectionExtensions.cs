using Microsoft.Extensions.DependencyInjection;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Mapping;
using SampleStack.AutoMapper.Profiles;
using SampleStack.AutoMapper.Services;

namespace SampleStack.AutoMapper.Configuration
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IMapService, MapService>();

            // Add DataSources
            services.AddScoped<IDataSource<CustomerDto>, CustomerDataSource>();
            services.AddScoped<IDataSource<OrderDto>, OrderDataSource>();
            services.AddScoped<IDataSource<ProductDto>, ProductDataSource>();

            // Add Services
            services.AddSingleton<OrderService>();
            services.AddSingleton<ProductService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<DataRetrievalFacade>();
        }
    }
}
