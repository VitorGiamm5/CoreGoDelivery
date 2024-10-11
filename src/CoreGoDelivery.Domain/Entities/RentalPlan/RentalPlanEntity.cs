using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.RentalPlan
{
    public class RentalPlanEntity
    {
        [Key]
        public int RentalPlanId { get; set; }

        public int Days { get; set; }

        public double DailyCost { get; set; }
    }
}
