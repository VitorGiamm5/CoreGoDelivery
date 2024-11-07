using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMQConsumer(
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
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Backoff exponencial
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Console.WriteLine($"Tentativa {attempt} de conexão com RabbitMQ falhou: {exception.Message}. Tentando novamente em {timespan}.");
                });

        await retryPolicy.ExecuteAsync(() =>
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "motorcycle_queue",
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
                IdMotorcycle = notification.IdMotorcycle,
                YearManufacture = notification.YearManufacture,
                DateCreated = notification.CreatedAt,
            };

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationMotorcycleRepository>();
                notificationRepository.Create(entityNotification);
            }

            Console.WriteLine("Mensagem recebida:");
            Console.WriteLine($"Id: {notification.Id}");
            Console.WriteLine($"IdMotorcycle: {notification.IdMotorcycle}");
            Console.WriteLine($"YearManufacture: {notification.YearManufacture}");
            Console.WriteLine($"CreatedAt: {notification.CreatedAt}");
        };

        _channel.BasicConsume(queue: "motorcycle_queue",
                              autoAck: true,
                              consumer: consumer);

        Console.WriteLine("Consumidor aguardando mensagens da fila 'motorcycle_queue'...");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
