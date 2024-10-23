using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public class RentalCreateMappers
    {
        public RentalEntity MapCreateToEntity(RentalCreateCommand data, RentalEntity? rentalDates)
        {
            if (rentalDates == null)
            {
                return new RentalEntity();
            }

            var result = new RentalEntity()
            {
                Id = Ulid.NewUlid().ToString(),
                StartDate = rentalDates!.StartDate,
                EndDate = rentalDates.EndDate,
                EstimatedReturnDate = rentalDates.EstimatedReturnDate,
                ReturnedToBaseDate = null,
                DeliverierId = data?.DeliverierId,
                MotorcycleId = data?.MotorcycleId,
                RentalPlanId = data!.PlanId
            };

            return result;
        }
    }
}
