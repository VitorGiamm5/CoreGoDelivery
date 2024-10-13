using CoreGoDelivery.Domain.Entities.Deliverier;
using CoreGoDelivery.Domain.Entities.Motocycle;
using CoreGoDelivery.Domain.Entities.RentalPlan;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGoDelivery.Domain.Entities.Rental
{
    public class RentalEntity
    {
        [Key]
        public string RentalId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EstimatedEndDate { get; set; }

        public DateTime? ReturnedToBaseDate { get; set; }


        #region Relationships

        [ForeignKey(nameof(Deliverier))]
        public string DeliveryPersonId { get; set; }
        public DeliverierEntity? Deliverier { get; set; }

        [ForeignKey(nameof(Motocycle))]
        public string MotocycleId { get; set; }
        public MotocycleEntity? Motocycle { get; set; }

        [ForeignKey(nameof(RentalPlan))]
        public int RentalPlanId { get; set; }
        public RentalPlanEntity? RentalPlan { get; set; }

        #endregion
    }
}
