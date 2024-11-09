using CoreGoDelivery.Domain;
using CoreGoDelivery.Domain.MinIOFile;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO;
using CoreGoDelivery.Infrastructure.Repositories.GoDelivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
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
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Console.WriteLine($"Retry {attempt} fail with error: {exception.Message}. Lets try again {timespan}.");
                });

        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            retryPolicy.Execute(() =>
            {
                options.UseNpgsql()
                       .AddInfrastructure(configuration);
            });
        });

        AddRepositories(services);

        services.Configure<MinIOSettings>(options => configuration.GetSection("MinIOSettings"));

        services.AddSingleton<MinioClient>();

        services.TryAddTransient<IMinIOFileService, MinIOFileService>();

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
