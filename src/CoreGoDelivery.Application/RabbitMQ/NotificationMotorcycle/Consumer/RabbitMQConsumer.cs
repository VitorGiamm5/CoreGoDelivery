using CoreGoDelivery.Application.RabbitMQ.Settings;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;
using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer
{
    public class RabbitMQConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly INotificationMotorcycleRepository _repository;

        public RabbitMQConsumer(IOptions<RabbitMQSettings> options, IServiceScopeFactory serviceScopeFactory, INotificationMotorcycleRepository repository)
        {
            var settings = options.Value;
            _factory = new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password
            };

            _serviceScopeFactory = serviceScopeFactory;
            _repository = repository;
        }

        public void ConsumeMotorcycleQueue()
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

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

                // Desserializar a mensagem
                var motorcycle = JsonSerializer.Deserialize<NotificationMotorcycleEntity>(message);

                // Criar um escopo para acessar o DbContext ou qualquer serviço Scoped
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    if (motorcycle != null)
                    {
                        _repository.Create(motorcycle);
                        // Persistir os dados no banco de dados
                        //dbContext.NotificationMotorcycles.Add(motorcycle);
                        //dbContext.SaveChanges();
                    }
                }
            };

            channel.BasicConsume(queue: "motorcycle_queue", autoAck: true, consumer: consumer);

            Console.WriteLine("Aguardando mensagens...");
            Console.ReadLine();  // Manter o consumidor ativo
        }
    }
}

