using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public class RentalCreateMappers
    {
        public RentalEntity MapCreateToEntity(RentalCreateCommand data, RentalEntity RentalDates)
        {
            var result = new RentalEntity()
            {
                Id = Ulid.NewUlid().ToString(),
                StartDate = RentalDates.StartDate,
                EndDate = RentalDates.EndDate,
                EstimatedReturnDate = RentalDates.EstimatedReturnDate,
                ReturnedToBaseDate = null,
                DeliverierId = data?.DeliverierId,
                MotorcycleId = data?.MotorcycleId,
                RentalPlanId = data!.PlanId
            };

            return result;
        }
    }
}
