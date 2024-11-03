using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete;

public class MotorcycleDeleteCommand : IRequest<ApiResponse>
{
    public MotorcycleDeleteCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
