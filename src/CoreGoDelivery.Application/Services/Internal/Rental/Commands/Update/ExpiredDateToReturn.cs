using CoreGoDelivery.Domain.Consts;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update
{
    public class ExpiredDateToReturn
    {
        public static StringBuilder Calculate(int diffDays, StringBuilder message)
        {
            var penaltyValue = diffDays * RentalServiceConst.PENALTY_VALUE_PER_DAY;

            message.Append($"{penaltyValue}");

            return message;
        }
    }
}
