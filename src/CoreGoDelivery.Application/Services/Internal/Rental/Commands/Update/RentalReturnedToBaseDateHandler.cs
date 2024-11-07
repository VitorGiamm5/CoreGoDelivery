﻿using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;

public class RentalReturnedToBaseDateHandler : IRequestHandler<RentalReturnedToBaseDateCommand, ActionResult>
{
    public readonly IRentalRepository _repositoryRental;

    public readonly RentalReturnedToBaseValidator _validator;

    public RentalReturnedToBaseDateHandler(
        IRentalRepository repositoryRental,
        RentalReturnedToBaseValidator validator)
    {
        _repositoryRental = repositoryRental;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(RentalReturnedToBaseDateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetMessage(await _validator.BuilderUpdateValidator(request));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var rental = await _repositoryRental.GetByIdAsync(request.Id);

        var returnedDate = request!.ReturnedToBaseDate!.Value;

        apiReponse = RentalCalculatePenalty.Calculate(returnedDate, rental);

        var successUpdate = await _repositoryRental.UpdateReturnedToBaseDate(request.Id, returnedDate);

        if (!successUpdate)
        {
            apiReponse.Data = null;
            apiReponse.SetMessage(nameof(request.ReturnedToBaseDate).AppendError(AdditionalMessageEnum.UpdateFail));

            return apiReponse;
        }

        return apiReponse;
    }
}
