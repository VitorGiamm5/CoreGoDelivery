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

            services.AddDbContextPool<ApplicationDbContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("postgres"))
                .AddInfrastructure(configuration));

            services.TryAddScoped<IDeliverierRepository, DeliverierRepository>();
            services.TryAddScoped<ILicenceDriverRepository, LicenceDriverRepository>();
            services.TryAddScoped<IModelMotorcycleRepository, ModelMotorcycleRepository>();
            services.TryAddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.TryAddScoped<IRentalPlanRepository, RentalPlanRepository>();
            services.TryAddScoped<IRentalRepository, RentalRepository>();
            services.TryAddScoped<INotificationMotorcycleRepository, NotificationMotorcycleRepository>();

            return services;
        }
    }
}
