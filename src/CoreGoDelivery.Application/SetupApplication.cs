using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreGoDelivery.Application
{
    public static class SetupApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructure(configuration);

            services
                .MessageBuildValidator();

            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        private static IServiceCollection MessageBuildValidator(this IServiceCollection services)
        {
            services
                .AddScoped<IBaseInternalServices, BaseInternalServices>();

            return services;
        }
    }
}
