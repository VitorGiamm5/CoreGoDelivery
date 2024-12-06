using CoreGoDelivery.Domain.Response.BaseResponse;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public sealed class MotorcycleCreateCommand : IRequest<ActionResult>
{
    [JsonIgnore]
    public string Id { get; set; } = Ulid.NewUlid().ToString();

    [DefaultValue(2020)]
    public int YearManufacture { get; set; }

    [DefaultValue("Sport")]
    public string ModelName { get; set; }

    [DefaultValue("CDX-0101")]
    public string Plate { get; set; }
}
