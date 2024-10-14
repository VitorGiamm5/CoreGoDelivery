using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Domain
{
    public static class SetupDomain
    {
        public static IConfiguration AddDomain(this IConfiguration configuration)
        {
            return configuration;
        }
    }
}
