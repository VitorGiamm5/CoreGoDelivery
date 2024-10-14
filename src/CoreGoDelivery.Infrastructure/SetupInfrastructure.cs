using CoreGoDelivery.Domain;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Infrastructure
{
    public static class SetupInfrastructure 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDomain(configuration);
            services.AddDbContextPool<GoDeliveryContext>(options => options.AddInfrastructure(configuration));
        
            return services;
        }
    }
}
