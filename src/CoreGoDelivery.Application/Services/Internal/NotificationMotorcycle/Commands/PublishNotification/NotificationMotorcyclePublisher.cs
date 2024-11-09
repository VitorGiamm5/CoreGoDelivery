using CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.Services.Internal.NotificationMotorcycle.Commands.PublishNotification;

public class NotificationMotorcyclePublisher
{
    private readonly IConnection _connection;

    private const string MOTORCYCLE_QUEUE = "motorcycle_queue";

    public NotificationMotorcyclePublisher(IConnection connection)
    {
        _connection = connection;
    }

    public void PublishMotorcycle(NotificationMotorcycleDto motorcycle)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: MOTORCYCLE_QUEUE,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var message = JsonSerializer.Serialize(motorcycle);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: MOTORCYCLE_QUEUE,
                             basicProperties: null,
                             body: body);

        Console.WriteLine(" [x] Published: {0}", message);
    }
}
