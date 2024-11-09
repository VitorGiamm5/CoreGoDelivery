using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace CoreGoDelivery.Application.Services.External.NotificationMotorcycle.Queries;

public class NotificationMotorcycleConsumer : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IModel? _channel;

    private const string MOTORCYCLE_QUEUE = "motorcycle_queue";

    public NotificationMotorcycleConsumer(
        IConnectionFactory connectionFactory,
        IServiceScopeFactory serviceScopeFactory)
    {
        _connectionFactory = connectionFactory;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var retryPolicy = Policy
            .Handle<BrokerUnreachableException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Console.WriteLine($"Retry {attempt} connection to RabbitMQ failed: {exception.Message}. lets try again {timespan}.");
                });

        await retryPolicy.ExecuteAsync(() =>
        {
            var connection = _connectionFactory.CreateConnection();

            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: MOTORCYCLE_QUEUE,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            return Task.CompletedTask;
        });

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var notification = JsonConvert.DeserializeObject<NotificationMotorcycleDto>(message)!;

            var entityNotification = new NotificationMotorcycleEntity
            {
                Id = notification.Id,
                IdMotorcycle = notification.MotorcycleId,
                YearManufacture = notification.YearManufacture,
                DateCreated = notification.DateCreated,
                CreatedBy = notification.CreatedBy
            };

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationMotorcycleRepository>();
                notificationRepository.Create(entityNotification);
            }

            Console.WriteLine("Message recipted:");
            Console.WriteLine($"Id: {notification.Id}");
            Console.WriteLine($"IdMotorcycle: {notification.MotorcycleId}");
            Console.WriteLine($"YearManufacture: {notification.YearManufacture}");
            Console.WriteLine($"CreatedAt: {notification.DateCreated}");
        };

        _channel.BasicConsume(queue: "motorcycle_queue",
                              autoAck: true,
                              consumer: consumer);

        Console.WriteLine("Consummer pooling queue: 'motorcycle_queue'...");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
