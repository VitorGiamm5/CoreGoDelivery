using CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.Services.Internal.NotificationMotorcycle.Commands.PublishNotification;

public class NotificationMotorcyclePublisher
{
    private readonly IConnection _connection;
    private readonly string _publisherQueueName;

    public NotificationMotorcyclePublisher(IConnection connection, IConfiguration configuration)
    {
        _connection = connection;
        _publisherQueueName = configuration["RabbitMQ:QueuesName:MotorcycleNotificationPublishQueue"]
                              ?? throw new BrokerUnreachableException(new Exception("RabbitMQ queue name for publisher is not configured."));
    }

    public void PublishMotorcycle(NotificationMotorcycleDto motorcycle)
    {
        ArgumentNullException.ThrowIfNull(motorcycle);

        try
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: _publisherQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(motorcycle);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: _publisherQueueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"[x] Published message to {_publisherQueueName}: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error publishing message to {_publisherQueueName}: {ex.Message}");
            throw;
        }
    }
}
