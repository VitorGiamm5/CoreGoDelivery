using CoreGoDelivery.Domain.Consts;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;

public class RentalExpiredDateToReturn
{
    public static StringBuilder Calculate(int diffDays, StringBuilder message)
    {
        var penaltyValue = diffDays * RentalServiceConst.PENALTY_VALUE_PER_DAY;

        message.Append($"{penaltyValue}");

        return message;
    }
}
