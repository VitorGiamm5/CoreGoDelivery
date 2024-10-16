namespace CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan
{
    public sealed class RentalPlanEntity
    {
        public int Id { get; set; }
        public int DaysQuantity { get; set; }
        public double DayliCost { get; set; }
    }
}
