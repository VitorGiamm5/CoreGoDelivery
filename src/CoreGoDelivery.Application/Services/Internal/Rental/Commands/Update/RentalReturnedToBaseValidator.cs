using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;

public class RentalReturnedToBaseValidator
{
    public readonly IRentalRepository _repositoryRental;

    public RentalReturnedToBaseValidator(IRentalRepository repositoryRental)
    {
        _repositoryRental = repositoryRental;
    }

    public async Task<StringBuilder> BuilderUpdateValidator(RentalReturnedToBaseDateCommand? data)
    {
        var message = new StringBuilder();

        string idRental = data!.Id!;

        #region Id validator

        var rentalEntity = await _repositoryRental.GetByIdAsync(idRental);

        if (rentalEntity == null)
        {
            message.Append(nameof(idRental).AppendError(AdditionalMessageEnum.NotFound));

            return message;
        }
        #endregion

        #region Check if is Returned validator

        var isReturned = await _repositoryRental.CheckisReturnedById(idRental);

        if (isReturned)
        {
            message.Append($"Invalid field: {nameof(rentalEntity.ReturnedToBaseDate)} was returned previously at: {rentalEntity.ReturnedToBaseDate}; ");

            return message;
        }

        #endregion

        #region Returned To Base Date validator

        var isBeforeDateStart = data.ReturnedToBaseDate > rentalEntity.StartDate;

        if (!isBeforeDateStart)
        {
            message.Append($"Invalid field: {nameof(data.ReturnedToBaseDate)} : {data.ReturnedToBaseDate} must be after 'StartDate' : {rentalEntity.StartDate}; ");
        
            return message;
        }

        #endregion

        return message;
    }
}
