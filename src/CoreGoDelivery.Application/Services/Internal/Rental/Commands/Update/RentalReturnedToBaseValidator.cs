using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;

public class RentalReturnedToBaseValidator
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IRentalRepository _repositoryRental;

    public RentalReturnedToBaseValidator(IBaseInternalServices baseInternalServices, IRentalRepository repositoryRental)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryRental = repositoryRental;
    }

    public async Task<string?> BuilderUpdateValidator(RentalReturnedToBaseDateCommand? data)
    {
        var message = new StringBuilder();

        var idRental = data!.Id;

        #region Id validator

        var isValidIdParam = _baseInternalServices.RequestIdParamValidator(idRental);

        if (!isValidIdParam)
        {
            message.Append(nameof(idRental));

            return message.ToString();
        }

        var rentalEntity = await _repositoryRental.GetByIdAsync(idRental);

        if (rentalEntity == null)
        {
            message.Append(nameof(idRental).AppendError(AdditionalMessageEnum.NotFound));
        }
        else
        {
            var isReturned = await _repositoryRental.CheckisReturnedById(data.Id);

            if (isReturned)
            {
                message.Append($"Invalid field: {nameof(rentalEntity.ReturnedToBaseDate)} was returned previously at: {rentalEntity.ReturnedToBaseDate}; ");
            }
        }

        #endregion

        #region Returned To Base Date validator

        if (data.ReturnedToBaseDate == null)
        {
            message.Append(nameof(data.ReturnedToBaseDate));
        }
        else
        {
            var isAfterDateStart = data.ReturnedToBaseDate >= rentalEntity?.StartDate;

            if (!isAfterDateStart)
            {
                message.Append($"Invalid field: {nameof(data.ReturnedToBaseDate)} : {data.ReturnedToBaseDate} must be after 'StartDate' : {rentalEntity?.StartDate}; ");
            }
        }

        #endregion

        return _baseInternalServices.BuildMessageValidator(message);
    }
}
