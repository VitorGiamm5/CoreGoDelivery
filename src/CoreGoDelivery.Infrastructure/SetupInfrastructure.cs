using CoreGoDelivery.Domain;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using CoreGoDelivery.Infrastructure.Repositories.GoDelivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddScoped<IDeliverierRepository, DeliverierRepository>();
            services.AddScoped<IMotocycleRepository, MotocycleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            //var serviceProvider = services.BuildServiceProvider();

            //using (var scope = serviceProvider.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

            //    try
            //    {
            //        dbContext.Database.Migrate();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log ou tratamento de erro
            //        Console.WriteLine($"Erro ao aplicar as migrações: {ex.Message}");
            //        throw;
            //    }
            //}
            return services;
        }
    }
}
