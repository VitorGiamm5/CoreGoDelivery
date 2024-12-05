﻿using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public class RentalGetOneHandler : IRequestHandler<RentalGetOneCommand, ActionResult>
{
    public readonly IRentalRepository _repositoryRental;

    public RentalGetOneHandler(
        IRentalRepository repositoryRental)
    {
        _repositoryRental = repositoryRental;
    }

    public async Task<ActionResult> Handle(RentalGetOneCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        var idRental = request.Id;

        var rental = await _repositoryRental.GetByIdAsync(idRental);

        if (rental == null)
        {
            apiReponse.SetError(new { });

            return apiReponse;
        }

        var rentalDto = RentalGetOneMappers.RentalEntityToDto(rental);

        apiReponse.SetData(rental);

        return apiReponse;
    }
}
