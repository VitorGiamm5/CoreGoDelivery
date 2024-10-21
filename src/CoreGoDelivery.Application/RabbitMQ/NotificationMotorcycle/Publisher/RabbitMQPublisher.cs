using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher
{
    public class RabbitMQPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            var rabbitMQHost = configuration["RabbitMQ:Host"];
            var rabbitMQPort = int.Parse(configuration["RabbitMQ:Port"]!);
            var rabbitMQUsername = configuration["RabbitMQ:Username"];
            var rabbitMQPassword = configuration["RabbitMQ:Password"];

            _factory = new ConnectionFactory
            {
                HostName = rabbitMQHost,
                Port = rabbitMQPort,
                UserName = rabbitMQUsername,
                Password = rabbitMQPassword
            };
        }

        public void PublishMotorcycle(NotificationMotorcycleDto motorcycle)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

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
}
