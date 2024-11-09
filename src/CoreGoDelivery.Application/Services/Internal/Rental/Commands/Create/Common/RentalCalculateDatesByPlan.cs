using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;

public static class RentalCalculateDatesByPlan
{
    public static RentalEntity Calculate(RentalPlanEntity plan)
    {
        var result = new RentalEntity()
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(plan.DaysQuantity),
            EstimatedReturnDate = DateTime.UtcNow.AddDays(plan.DaysQuantity)
        };

        return result;
    }
}
