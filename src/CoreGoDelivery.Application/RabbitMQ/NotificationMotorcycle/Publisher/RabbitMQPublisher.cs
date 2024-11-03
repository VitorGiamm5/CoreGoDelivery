using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;

public class RabbitMQPublisher
{
    private readonly IConnection _connection;

    public RabbitMQPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public void PublishMotorcycle(NotificationMotorcycleDto motorcycle)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: "motorcycle_queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var message = JsonSerializer.Serialize(motorcycle);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "motorcycle_queue",
                             basicProperties: null,
                             body: body);

        Console.WriteLine(" [x] Publicado: {0}", message);
    }
}
