using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Rental
{
    public sealed class RentalEntity
    {
        [Key]
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EstimatedReturnDate { get; set; }

        public DateTime? ReturnedToBaseDate { get; set; }


        #region Relationships

        [ForeignKey(nameof(Deliverier))]
        public string DeliverierId { get; set; }
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
