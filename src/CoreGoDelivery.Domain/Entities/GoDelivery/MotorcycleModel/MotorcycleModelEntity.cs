using CoreGoDelivery.Domain.Entities.GoDelivery.Base;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.MotorcycleModel;

public sealed class MotorcycleModelEntity : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
}
