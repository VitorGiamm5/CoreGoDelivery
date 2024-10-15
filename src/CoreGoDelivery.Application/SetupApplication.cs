using CoreGoDelivery.Application.Services.Internal.Deliverier;
using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Application.Services.Internal.Motocycle;
using CoreGoDelivery.Application.Services.Internal.Rental;
using CoreGoDelivery.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Application
{
    public static class SetupApplication
    {
        private static IConfigurationRoot _config;

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
                .AddSingleton<IDeliverierService, DeliverierService>();

            services
                .AddSingleton<IMotocycleService, MotocycleService>();

            services
                .AddSingleton<IRentalService, RentalService>();

            return services;
        }
    }
}
