using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update
{
    public class CalculatePenalty
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public string? Calculate(DateTime returnedToBaseDate, RentalEntity? rental)
        {
            TimeSpan buildDiffDays = returnedToBaseDate - rental!.EstimatedReturnDate;

            int diffDays = buildDiffDays.Days;

            if (diffDays == 0)
            {
                return RentalServiceConst.MESSAGE_RETURNED_TO_BASE_SUCCESS;
            }

            var message = new StringBuilder();

            message.AppendLine($"Value to pay: {RentalServiceConst.CURRENCY_BRL} ");

            if (diffDays < 0)
            {
                message = ReturnerBeforeExpected.Calculate(rental, diffDays, message);
            }
            else
            {
                message = ExpiredDateToReturn.Calculate(diffDays, message);
            }

            var result = _baseInternalServices.BuildMessageValidator(message);

            return result;
        }
    }
}
