using CoreGoDelivery.Domain.Entities.GoDelivery.Base;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotorcycle;

public sealed class ModelMotorcycleEntity : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
}
