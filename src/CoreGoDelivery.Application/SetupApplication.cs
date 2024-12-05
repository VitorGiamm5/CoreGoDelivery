using CoreGoDelivery.Application.Services.External.NotificationMotorcycle.Queries.Consumer;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete;
using CoreGoDelivery.Application.Services.Internal.NotificationMotorcycle.Commands.PublishNotification;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;
using CoreGoDelivery.Domain.RabbitMQ;
using CoreGoDelivery.Infrastructure;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using RabbitMQ.Client;
using System.Reflection;

namespace CoreGoDelivery.Application;

public static class SetupApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.BuildMessageValidator();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQ").Bind(options));

        services.TryAddSingleton<IConnectionFactory>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Host"],
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                Port = int.Parse(configuration["RabbitMQ:Port"]!)
            };

            return factory;
        });

        services.AddSingleton<IConnection>(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();
            var policy = Polly.Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} for RabbitMQ connection: {exception.Message}");

                    Console.WriteLine(
                        $"HostName: '{configuration["RabbitMQ:Host"]}'," +
                        $"UserName: '{configuration["RabbitMQ:Username"]}'," +
                        $"Password: '{configuration["RabbitMQ:Password"]}'," +
                        $"Port: '{configuration["RabbitMQ:Port"]}'");

                    Console.WriteLine(
                        $"Publish: '{configuration["RabbitMQ:QueuesName:MotorcycleNotificationPublishQueue"]}'," +
                        $"Consumer: '{configuration["RabbitMQ:QueuesName:MotorcycleNotificationConsumerQueue"]}'");

                });
            return policy.Execute(() => factory.CreateConnection());
        });

        services.TryAddTransient<NotificationMotorcyclePublisher>();
        services.AddHostedService<NotificationMotorcycleConsumer>();

        return services;
    }

    private static IServiceCollection BuildMessageValidator(this IServiceCollection services)
    {
        AddRentalServices(services);
        AddDeliverierServices(services);
        AddMotorcycleServices(services);

        return services;
    }

    private static void AddMotorcycleServices(IServiceCollection services)
    {
        services.TryAddScoped<MotorcycleChangePlateValidator>();
        services.TryAddScoped<MotorcycleCreateValidator>();
        services.TryAddScoped<MotorcycleDeleteValidator>();
    }

    private static void AddDeliverierServices(IServiceCollection services)
    {
        services.TryAddScoped<DeliverierBuildMessageCnh>();
        services.TryAddScoped<DeliverierBuildMessageDeliverierCreate>();
        services.TryAddScoped<DeliverierCreateValidator>();
        services.TryAddScoped<LicenseDriverValidator>();
    }

    private static void AddRentalServices(IServiceCollection services)
    {
        services.TryAddScoped<RentalBuildMessageDeliverierId>();
        services.TryAddScoped<RentalBuildMessageMotorcycleId>();
        services.TryAddScoped<RentalBuildMessagePlanId>();
        services.TryAddScoped<RentalCreateValidate>();
        services.TryAddScoped<RentalReturnedToBaseValidator>();
    }
}
