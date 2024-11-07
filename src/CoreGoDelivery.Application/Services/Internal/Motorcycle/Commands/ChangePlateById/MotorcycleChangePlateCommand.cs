using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;

public class MotorcycleChangePlateCommand : IRequest<ActionResult>
{
    [JsonIgnore]
    public string? Id { get; set; }

    [DefaultValue("abc-1234")]
    public string? Plate { get; set; }
}
