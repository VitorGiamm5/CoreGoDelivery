using CoreGoDelivery.Domain.Entities.GoDelivery.Base;

namespace CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;

public sealed class NotificationMotorcycleDto : BaseEntity
{
    public string Id { get; set; }
    public string ModelMotorcycleId { get; set; }
    public string PlateNormalized { get; set; }
    public string MotorcycleId { get; set; }
    public int YearManufacture { get; set; }
}
