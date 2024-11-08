
using CoreGoDelivery.Application.Services.External.NotificationMotorcycle.Queries;
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
                Port = 5672
            };

            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                factory.HostName = "rabbitmq";
                factory.UserName = "guest";
                factory.Password = "guest";
            }

            return factory;
        });

        services.AddSingleton<IConnection>(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();
            return factory.CreateConnection();
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
