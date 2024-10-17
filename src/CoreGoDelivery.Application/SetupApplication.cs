using CoreGoDelivery.Application.Services.Internal.Deliverier;
using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Application.Services.Internal.Motorcycle;
using CoreGoDelivery.Application.Services.Internal.Rental;
using CoreGoDelivery.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Application
{
    public static class SetupApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructure(configuration);

            services
               .AddInternalServices();

            return services;
        }

        private static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services
                .AddScoped<IDeliverierService, DeliverierService>();

            services
                .AddScoped<IMotocycleService, MotorcycleService>();

            services
                .AddScoped<IRentalService, RentalService>();

            return services;
        }
    }
}
