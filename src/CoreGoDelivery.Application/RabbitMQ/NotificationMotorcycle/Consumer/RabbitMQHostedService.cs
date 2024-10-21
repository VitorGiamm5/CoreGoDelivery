using Microsoft.Extensions.Hosting;

namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly RabbitMQConsumer _consumer;

        public RabbitMQConsumerService(RabbitMQConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => _consumer.ConsumeMotorcycleQueue(), stoppingToken);
        }
    }
}
