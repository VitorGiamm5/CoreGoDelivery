using CoreGoDelivery.Domain.DTO.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List
{
    public class MotorcycleListQueryCommand : IRequest<ApiResponse>
    {
        [DefaultValue("abc-1234")]
        [JsonPropertyName("placa")]
        public string? Plate { get; set; }
    }
}
