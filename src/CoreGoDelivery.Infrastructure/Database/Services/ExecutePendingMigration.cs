using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Infrastructure.Database.Services
{
    public class ExecutePendingMigration
    {
        public static void Execute(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                var migrations = dbContext.Database.GetPendingMigrations();

                if (migrations.Any())
                {
                    dbContext.Database.MigrateAsync().Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to apply migrations: {ex.Message}");

                throw;
            }
        }
    }
}
