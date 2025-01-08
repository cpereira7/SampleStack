using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleStack.Repository.Repositories;
using SampleStack.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleStack.Repository
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var isDevelopment = environment.IsDevelopment();

            if (isDevelopment)
            {
                services.AddSingleton(typeof(IRepository<>), typeof(MemoryRepository<>));
            }
            else
            {
                services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
                var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection string not found");

                Console.WriteLine($"Connection string: {connectionString}");

                services.AddDbContext<DbContext, ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            }

            services.AddSingleton<IPersonService, PersonService>();
        }
    }
}
