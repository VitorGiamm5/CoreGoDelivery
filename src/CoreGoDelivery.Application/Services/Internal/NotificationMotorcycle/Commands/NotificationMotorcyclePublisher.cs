using CoreGoDelivery.Domain.RabbitMQ;
using CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.Services.Internal.NotificationMotorcycle.Commands;

public class NotificationMotorcyclePublisher
{
    private readonly IConnection _connection;
    private readonly RabbitMQSettings _settings;

    public NotificationMotorcyclePublisher(IConnection connection, IOptions<RabbitMQSettings> settings)
    {
        _connection = connection;
        _settings = settings.Value;
    }

    public void PublishMotorcycle(NotificationMotorcycleDto motorcycle)
    {
        using var channel = _connection.CreateModel();

        var queueName = _settings.QueuesName.MotorcycleQueue;

        channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var message = JsonSerializer.Serialize(motorcycle);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);

        Console.WriteLine(" [x] Published: {0}", message);
    }
}
