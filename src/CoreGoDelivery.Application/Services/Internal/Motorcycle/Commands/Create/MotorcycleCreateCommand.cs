using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public sealed class MotorcycleCreateCommand : IRequest<ApiResponse>
{
    [DefaultValue("moto123")]
    public string Id { get; set; }

    [DefaultValue(2020)]
    public int YearManufacture { get; set; }

    [DefaultValue("Mottu Sport")]
    public string ModelName { get; set; }

    [DefaultValue("CDX-0101")]
    [JsonPropertyName("placa")]
    public string PlateId { get; set; }
}
