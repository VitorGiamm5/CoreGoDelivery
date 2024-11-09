using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public static class RentalGetOneMappers
{
    public static object RentalEntityToDto(RentalEntity rental)
    {
        var restult = new
        {
            rental!.Id,
            rental.RentalPlan!.DayliCost,
            rental.DeliverierId,
            rental.MotorcycleId,
            rental.StartDate,
            rental.EndDate,
            rental.EstimatedReturnDate,
            rental.ReturnedToBaseDate,
        };

        return restult;
    }
}
