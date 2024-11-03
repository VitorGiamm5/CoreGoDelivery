using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Application.RabbitMQ.Settings;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;
using CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;
using CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne.BuildMessage;
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

        var isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Host"],
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                Port = 5672
            };

            if (isInDocker)
            {
                factory.HostName = "rabbitmq";
                factory.UserName = "guest";
                factory.Password = "guest";
            }

            return factory.CreateConnection();
        });

        services.TryAddSingleton<RabbitMQPublisher>();

        services.AddSingleton<RabbitMQConsumer>();

        return services;
    }

    private static IServiceCollection BuildMessageValidator(this IServiceCollection services)
    {
        AddBaseServices(services);

        AddRentalServices(services);

        AddDeliverierServices(services);

        AddMotorcycleServices(services);

        return services;
    }

    private static void AddBaseServices(IServiceCollection services)
    {
        services.TryAddScoped<IBaseInternalServices, BaseInternalServices>();
    }

    private static void AddMotorcycleServices(IServiceCollection services)
    {
        services.TryAddScoped<MotorcycleChangePlateValidator>();
        services.TryAddScoped<MotorcycleCreateNotification>();
        services.TryAddScoped<MotorcycleCreateValidator>();
        services.TryAddScoped<MotorcycleDeleteValidator>();
        services.TryAddScoped<MotorcycleServiceMappers>();
        services.TryAddScoped<MotorcyclePlateValidator>();
    }

    private static void AddDeliverierServices(IServiceCollection services)
    {
        services.TryAddScoped<DeliverierBuildMessageCnh>();
        services.TryAddScoped<DeliverierBuildMessageDeliverierCreate>();

        services.TryAddScoped<DeliverierBuildFileName>();
        services.TryAddScoped<DeliverierParseLicenseType>();
        services.TryAddScoped<DeliverierBuildMessageBirthDate>();
        services.TryAddScoped<DeliverierBuildMessageCnpj>();
        services.TryAddScoped<DeliverierBuildMessageFullName>();
        services.TryAddScoped<DeliverierBuildMessageLicenseType>();

        services.TryAddScoped<DeliverierCreateMappers>();
        services.TryAddScoped<DeliverierCreateValidator>();

        services.TryAddScoped<DeliverierSaveOrReplaceLicenseImageAsync>();
        services.TryAddScoped<DeliverierUploadCnhValidator>();

        services.TryAddScoped<DeliverierBuildExtensionFile>();
        services.TryAddScoped<DeliverierBuilderCreateImage>();
        services.TryAddScoped<DeliverierBuilderUpdateImage>();
    }

    private static void AddRentalServices(IServiceCollection services)
    {
        services.TryAddScoped<RentalCalculateDatesByPlan>();
        services.TryAddScoped<RentalPlanMotorcycleValidator>();
        services.TryAddScoped<RentalBuildMessageDeliverierId>();
        services.TryAddScoped<RentalBuildMessageMotorcycleId>();
        services.TryAddScoped<RentalBuildMessagePlanId>();
        services.TryAddScoped<RentalCreateMappers>();
        services.TryAddScoped<RentalCreateValidate>();

        services.TryAddScoped<RentalCalculatePenalty>();
        services.TryAddScoped<RentalExpiredDateToReturn>();
        services.TryAddScoped<RentalReturnerBeforeExpected>();
        services.TryAddScoped<RentalReturnedToBaseValidator>();

        services.TryAddScoped<RentalBuildMessageIdRental>();
        services.TryAddScoped<RentalGetOneMappers>();
    }
}
