using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public class RentalGetOneHandler : IRequestHandler<RentalGetOneCommand, ActionResult>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IRentalRepository _repositoryRental;


    public RentalGetOneHandler(
        IBaseInternalServices baseInternalServices,
        IRentalRepository repositoryRental)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryRental = repositoryRental;
    }

    public async Task<ActionResult> Handle(RentalGetOneCommand request, CancellationToken cancellationToken)
    {
        var message = new StringBuilder();

        var idRental = request.Id;

        var rental = await _repositoryRental.GetByIdAsync(idRental);

        var rentalDto = RentalGetOneMappers.RentalEntityToDto(rental);

        var apiReponse = new ActionResult()
        {
            Data = rentalDto,
        };

        apiReponse.SetMessage(message);

        return apiReponse;
    }
}
