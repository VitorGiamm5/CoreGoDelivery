using CoreGoDelivery.Domain.Consts;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;

public static class RentalExpiredDateToReturn
{
    public static double Calculate(int diffDays)
    {
        var penaltyValue = diffDays * RentalServiceConst.PENALTY_VALUE_PER_DAY;

        return penaltyValue;
    }
}
