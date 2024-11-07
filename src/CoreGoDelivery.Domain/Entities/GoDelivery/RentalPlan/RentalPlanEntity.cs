using CoreGoDelivery.Domain.Entities.GoDelivery.Base;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;

public sealed class RentalPlanEntity : BaseEntity
{
    public int Id { get; set; }
    public int DaysQuantity { get; set; }
    public double DayliCost { get; set; }
}
