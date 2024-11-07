using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public class RentalGetOneCommand : IRequest<ActionResult>
{
    public string Id { get; set; }
}
