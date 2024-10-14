using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan
{
    public sealed class RentalPlanEntity
    {
        [Key]
        public int RentalPlanId { get; set; }

        public int DaysQuantity { get; set; }

        public double DailyCost { get; set; }
    }
}
