using CoreGoDelivery.Domain.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create;

public class LicenseImageCommand : IRequest<ActionResult>
{
    public byte[] LicenseImageBase64 { get; set; }

    [JsonIgnore]
    public string? IdLicenseNumber { get; set; }
}
