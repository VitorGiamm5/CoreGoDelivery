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

#pragma warning disable CS1998 
        public async Task SendNotification(MotorcycleEntity motorcycle)
#pragma warning restore CS1998
        {
            if (motorcycle.YearManufacture == YEAR_MANUFACTORY_TO_SEND_MESSAGE)
            {
                var motorcycleNotification = new NotificationMotorcycleDto()
                {
                    Id = Ulid.NewUlid().ToString(),
                    IdMotorcycle = motorcycle.Id,
                    YearManufacture = motorcycle.YearManufacture,
                    CreatedAt = DateTime.UtcNow
                };

                _publisher.PublishMotorcycle(motorcycleNotification);

                Console.WriteLine("Motocicleta publicada na fila.");
            }
        }
    }
}
