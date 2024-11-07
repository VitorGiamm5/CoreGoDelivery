using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;

public static class RentalCreateMappers
{
    public static RentalEntity MapCreateToEntity(RentalCreateCommand data, RentalEntity? rentalDates)
    {
        if (rentalDates == null)
        {
            return new RentalEntity();
        }

        var result = new RentalEntity()
        {
            Id = data.Id,
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
