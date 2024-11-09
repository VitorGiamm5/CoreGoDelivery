using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;

public static class RentalReturnerBeforeExpected
{
    public static double Calculate(RentalEntity rental, int diffDays)
    {
        diffDays *= -1;

        var isMinimalDaysPlan = rental?.RentalPlan?.DaysQuantity == RentalServiceConst.MINIMAL_DAYS_PLAN;

        double feePercentPenalty = isMinimalDaysPlan
            ? RentalServiceConst.MINIMAL_FEE_PERCENTAGE / 100.0
            : RentalServiceConst.DEFAULT_FEE_PERCENTAGE / 100.0;

        var valueDaysRemain = rental!.RentalPlan!.DayliCost * diffDays;

        var penaltyValue = valueDaysRemain * feePercentPenalty;

        return penaltyValue;
    }
}
