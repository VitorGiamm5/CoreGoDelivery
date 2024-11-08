using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public sealed class MotorcycleCreateCommand : IRequest<ActionResult>
{
    [DefaultValue("moto123")]
    public string Id { get; set; }

    [DefaultValue(2020)]
    public int YearManufacture { get; set; }

    [DefaultValue("Mottu Sport")]
    public string ModelName { get; set; }

    [DefaultValue("CDX-0101")]
    public string Plate { get; set; }
}
