using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer;

public class RabbitMqConsumer
{
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMqConsumer(
        IConnection connection,
        IServiceScopeFactory serviceScopeFactory)
    {
        _connection = connection;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void ConsumeMessages()
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: "motorcycle_queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            NotificationMotorcycleDto notification = JsonConvert.DeserializeObject<NotificationMotorcycleDto>(message)!;

            var entityNotification = new NotificationMotorcycleEntity()
            {
                Id = notification.Id,
                IdMotorcycle = notification.IdMotorcycle,
                YearManufacture = notification.YearManufacture,
                CreatedAt = notification.CreatedAt,
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

        channel.BasicConsume(queue: "motorcycle_queue",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine("Consumidor aguardando mensagens da fila 'motorcycle_queue'...");
        Console.ReadLine();
    }
}



