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
                .UseNpgsql(configuration.GetConnectionString("postgre"))
                .AddInfrastructure(configuration));

            services.TryAddScoped<IDeliverierRepository, DeliverierRepository>();
            services.TryAddScoped<IMotocycleRepository, MotocycleRepository>();
            services.TryAddScoped<IRentalRepository, RentalRepository>();

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

                try
                {
                    var c = dbContext.Database.GetPendingMigrations();
                }
                catch (Exception ex)
                {
                    // Log ou tratamento de erro
                    Console.WriteLine($"Erro ao aplicar as migrações: {ex.Message}");
                    throw;
                }
            }

            return services;
        }
    }
}
