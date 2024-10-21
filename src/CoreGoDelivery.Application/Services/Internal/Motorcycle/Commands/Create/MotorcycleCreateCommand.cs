using CoreGoDelivery.Domain.DTO.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create
{
    public sealed class MotorcycleCreateCommand : IRequest<ApiResponse>
    {
        [DefaultValue("moto123")]
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [DefaultValue(2020)]
        [JsonPropertyName("ano")]
        public int YearManufacture { get; set; }

        [DefaultValue("Mottu Sport")]
        [JsonPropertyName("modelo")]
        public string ModelName { get; set; }

        [DefaultValue("CDX-0101")]
        [JsonPropertyName("placa")]
        public string PlateId { get; set; }
    }
}
