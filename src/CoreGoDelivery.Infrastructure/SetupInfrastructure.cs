using CoreGoDelivery.Domain;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using CoreGoDelivery.Infrastructure.Repositories.GoDelivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreGoDelivery.Infrastructure
{
    public static class SetupInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomain(configuration);

            services.AddDbContextPool<AplicationDbContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("Postgre"))
                .AddInfrastructure(configuration));

            services.TryAddScoped<IDeliverierRepository, DeliverierRepository>();
            services.TryAddScoped<ILicenceDriverRepository, LicenceDriverRepository>();
            services.TryAddScoped<IModelMotocycleRepository, ModelMotocycleRepository>();
            services.TryAddScoped<IMotocycleRepository, MotocycleRepository>();
            services.TryAddScoped<IRentalPlanRepository, RentalPlanRepository>();
            services.TryAddScoped<IRentalRepository, RentalRepository>();

            ExecutePendingMigration(services);

            return services;
        }

        private static void ExecutePendingMigration(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

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
                // Log ou tratamento de erro
                Console.WriteLine($"Erro ao aplicar as migrações: {ex.Message}");
                throw;
            }
        }
    }
}
