using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne;

public class MotorcycleGetOneQueryCommand : IRequest<ActionResult>
{
    public MotorcycleGetOneQueryCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
