using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Rental
{
    public sealed class RentalEntity
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedReturnDate { get; set; }
        public DateTime? ReturnedToBaseDate { get; set; }

        #region Relationships

        public string DeliverierId { get; set; }
        public DeliverierEntity? Deliverier { get; set; }
        public string MotorcycleId { get; set; }
        public MotorcycleEntity? Motorcycle { get; set; }
        public int RentalPlanId { get; set; }
        public RentalPlanEntity? RentalPlan { get; set; }

        #endregion
    }
}
