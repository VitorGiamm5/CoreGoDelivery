using CoreGoDelivery.Domain;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using CoreGoDelivery.Infrastructure.Repositories.GoDelivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace CoreGoDelivery.Infrastructure;

public static class SetupInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain(configuration);

        RetryPolicy retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Backoff exponencial
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Console.WriteLine($"Tentativa {attempt} falhou com erro: {exception.Message}. Tentando novamente em {timespan}.");
                });


        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            retryPolicy.Execute(() =>
            {
                options.UseNpgsql(configuration.GetConnectionString("postgres"))
                       .AddInfrastructure(configuration);
            });
        });

        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.TryAddScoped<IDeliverierRepository, DeliverierRepository>();
        services.TryAddScoped<ILicenceDriverRepository, LicenceDriverRepository>();
        services.TryAddScoped<IModelMotorcycleRepository, ModelMotorcycleRepository>();
        services.TryAddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.TryAddScoped<IRentalPlanRepository, RentalPlanRepository>();
        services.TryAddScoped<IRentalRepository, RentalRepository>();
        services.TryAddScoped<INotificationMotorcycleRepository, NotificationMotorcycleRepository>();
    }
}
