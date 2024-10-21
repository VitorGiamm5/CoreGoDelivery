using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Application.RabbitMQ.Settings;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Infrastructure;
using CoreGoDelivery.Infrastructure.Database;
using Fluent.Infrastructure.FluentModel;
using Microsoft.EntityFrameworkCore;
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

            // Registrar as configurações do RabbitMQ
            services
                .Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQ").Bind(options));

            // Registrar o RabbitMQPublisher como Singleton
            services
                .AddSingleton<RabbitMQPublisher>();

            // Registrar o RabbitMQConsumer como Singleton
            services
                .AddSingleton<RabbitMQConsumer>();

            // Registrar o HostedService para rodar o RabbitMQConsumer em background
            services
                .AddHostedService<RabbitMQConsumerService>();

            // Registrar o DbContext como Scoped
            //services
            //    .AddDbContext<ApplicationDbContext>(options => options
            //    .UseNpgsql(configuration.GetConnectionString("Postgre"))
            //    .AddInfrastructure(configuration));

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
