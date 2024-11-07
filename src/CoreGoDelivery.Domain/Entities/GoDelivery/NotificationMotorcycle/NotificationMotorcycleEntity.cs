using CoreGoDelivery.Domain.Entities.GoDelivery.Base;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;

public class NotificationMotorcycleEntity : BaseEntity
{
    public string Id { get; set; }
    public string IdMotorcycle { get; set; }
    public int YearManufacture { get; set; }
}
