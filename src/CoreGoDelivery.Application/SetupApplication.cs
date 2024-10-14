using CoreGoDelivery.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGoDelivery.Application;

public static class SetupApplication
{
    private static IConfigurationRoot _config;
    //private const string TEMPLATE_LOCAL_FILE = "appsettings.{0}.json";
    //public const string ASPNETENV = "ASPNETCORE_ENVIRONMENT";
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        services
            .AddSingleton(GetConfigurationRoot);

        services
            .AddInfrastructure(configuration);

        return services;
    }

    private static IConfigurationRoot GetConfigurationRoot(IServiceProvider provider)
    {
        _config = new ConfigurationBuilder()
            .Build();

        return _config;
    }
}
