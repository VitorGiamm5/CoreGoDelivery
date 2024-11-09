using CoreGoDelivery.Domain.Entities.GoDelivery.Base;
using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotorcycle;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;

public class MotorcycleEntity : BaseEntity
{
    public string Id { get; set; }
    public int YearManufacture { get; set; }
    public string PlateNormalized { get; set; }

    #region relationships

    [JsonIgnore]
    public string ModelMotorcycleId { get; set; }

    [JsonIgnore]
    public ModelMotorcycleEntity? ModelMotorcycle { get; set; }

    #endregion
}
