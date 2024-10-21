using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create
{
    public class MotorcycleCreateNotification
    {
        public readonly RabbitMQPublisher _publisher;

        private const int YEAR_MANUFACTORY_TO_SEND_MESSAGE = 2024;

        public MotorcycleCreateNotification(RabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task SendNotification(MotorcycleEntity motocycle)
        {
            if (motocycle.YearManufacture == YEAR_MANUFACTORY_TO_SEND_MESSAGE)
            {
                var motorcycleNotification = new NotificationMotorcycleDto()
                {
                    Id = Ulid.NewUlid().ToString(),
                    IdMotorcycle = motocycle.Id,
                    YearManufacture = motocycle.YearManufacture,
                    CreatedAt = DateTime.UtcNow
                };

                _publisher.PublishMotorcycle(motorcycleNotification);

                Console.WriteLine("Motocicleta publicada na fila.");
            }
        }
    }
}
