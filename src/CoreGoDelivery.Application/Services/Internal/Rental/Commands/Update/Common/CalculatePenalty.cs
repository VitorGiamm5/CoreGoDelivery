using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common
{
    public class CalculatePenalty
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public CalculatePenalty(IBaseInternalServices baseInternalServices)
        {
            _baseInternalServices = baseInternalServices;
        }

        public string? Calculate(DateTime returnedToBaseDate, RentalEntity? rental)
        {
            if (rental == null)
            {
                var messageError = new StringBuilder();

                messageError.AppendError(messageError,  nameof(rental), AdditionalMessageEnum.NotFound );

                return _baseInternalServices.BuildMessageValidator(messageError);
            }

            TimeSpan buildDiffDays = returnedToBaseDate - rental!.EstimatedReturnDate;

            int diffDays = buildDiffDays.Days;

            if (diffDays == 0)
            {
                return RentalServiceConst.MESSAGE_RETURNED_TO_BASE_SUCCESS;
            }

            var message = new StringBuilder();

            message.Append($"Value to pay: {RentalServiceConst.CURRENCY_BRL} ");

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
