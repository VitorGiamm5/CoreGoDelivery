using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common
{
    public class ReturnerBeforeExpected
    {
        public static StringBuilder Calculate(RentalEntity rental, int diffDays, StringBuilder message)
        {
            diffDays *= -1;

            var isPlanminimalPlan = rental?.RentalPlan?.DaysQuantity == RentalServiceConst.MINIMAL_DAYS_PLAN;

            double feePercentPenalty = isPlanminimalPlan
                ? RentalServiceConst.MINIMAL_FEE_PERCENTAGE / 100
                : RentalServiceConst.DEFAULT_FEE_PERCENTAGE / 100;

            var valueDaysRemain = rental!.RentalPlan!.DayliCost * diffDays;

            var penaltyValue = valueDaysRemain * feePercentPenalty;

            var penaltyValueRounded = Math.Round(penaltyValue, 2);

            message.Append($"{penaltyValueRounded}");

            return message;
        }
    }
}
