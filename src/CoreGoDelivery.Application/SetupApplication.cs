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
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Infrastructure;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;
using System.Reflection;

namespace CoreGoDelivery.Application
{
    public static class SetupApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            services.TryAddScoped<IBaseInternalServices, BaseInternalServices>();

            services.BuildMessageValidator();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Registrar as configurações do RabbitMQ
            services.Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQ").Bind(options));

            // Registrar o RabbitMQPublisher como Singleton

            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["RabbitMQ:Host"],
                    UserName = configuration["RabbitMQ:Username"],
                    Password = configuration["RabbitMQ:Password"],
                    Port = int.Parse(configuration["RabbitMQ:Port"]!)
                };
                return factory.CreateConnection();
            });
            services.TryAddSingleton<RabbitMQPublisher>();

            services.AddSingleton<RabbitMqConsumer>(); // Registrar o consumidor no DI

            // Registrar o DbContext como Scoped

            return services;
        }

        private static IServiceCollection BuildMessageValidator(this IServiceCollection services)
        {
            services.TryAddScoped<CalculateDatesByPlan>();
            services.TryAddScoped<PlanMotorcycleValidator>();
            services.TryAddScoped<BuildMessageDeliverierId>();
            services.TryAddScoped<BuildMessageMotorcycleId>();
            services.TryAddScoped<BuildMessagePlanId>();
            services.TryAddScoped<RentalCreateMappers>();
            services.TryAddScoped<RentalCreateValidate>();

            services.TryAddScoped<CalculatePenalty>();
            services.TryAddScoped<ExpiredDateToReturn>();
            services.TryAddScoped<ReturnerBeforeExpected>();
            services.TryAddScoped<RentalReturnedToBaseValidator>();

            services.TryAddScoped<BuildMessageIdRental>();
            services.TryAddScoped<RentalGetOneMappers>();

            services.TryAddScoped<BuildMessageCnh>();
            services.TryAddScoped<BuildMessageDeliverierCreate>();

            services.TryAddScoped<NormalizeFileNameLicense>();
            services.TryAddScoped<ParseLicenseType>();
            services.TryAddScoped<BuildMessageBirthDate>();
            services.TryAddScoped<BuildMessageCnpj>();
            services.TryAddScoped<BuildMessageFullName>();
            services.TryAddScoped<BuildMessageLicenseType>();
            services.TryAddScoped<DeliverierCreateMappers>();
            services.TryAddScoped<DeliverierCreateValidator>();
            services.TryAddScoped<MotorcycleChangePlateValidator>();
            services.TryAddScoped<MotorcycleCreateNotification>();
            services.TryAddScoped<MotorcycleCreateValidator>();
            services.TryAddScoped<MotorcycleDeleteValidator>();
            services.TryAddScoped<MotorcycleServiceMappers>();
            services.TryAddScoped<PlateValidator>();

            services.TryAddScoped<ValidateLicenseImage>();
            services.TryAddScoped<SaveOrReplaceLicenseImageAsync>();
            services.TryAddScoped<DeliverierUploadCnhValidator>();

            services.TryAddScoped<GetExtensionFile>();

            return services;
        }
    }
}
