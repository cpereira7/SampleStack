using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleStack.Repository
{
    internal static class DatabaseInitializer
    {
        public static void InitializeDataBase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var environment = services.GetRequiredService<IHostEnvironment>();
                if (!environment.IsDevelopment())
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while initializing the database. {0}", ex.Message);
            }
        }
    }
}
