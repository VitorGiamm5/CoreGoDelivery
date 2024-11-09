using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQueryCommand : IRequest<ActionResult>
{
    [DefaultValue("abc-1234")]
    public string? Plate { get; set; }
}
