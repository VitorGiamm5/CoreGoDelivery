using CoreGoDelivery.Domain.DTO.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public sealed class RentalCreateCommand : IRequest<ApiResponse>
    {
        [JsonIgnore]
        [DefaultValue("locacao123")]
        [JsonPropertyName("identification")]

        public string? RentalId { get; set; }

        [DefaultValue(10)]
        [JsonPropertyName("valor_diaria")]
        public double? DayliCost { get; set; }

        [DefaultValue("entregador123")]
        [JsonPropertyName("entregador_id")]
        public string DeliverierId { get; set; }

        [DefaultValue("moto123")]
        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [DefaultValue("2024-01-01T00:00:00Z")]
        [JsonPropertyName("data_inicio")]
        public string? StartDate { get; set; }

        [DefaultValue("2024-01-07T23:59:59Z")]
        [JsonPropertyName("data_termino")]
        public string? EndDate { get; set; }

        [DefaultValue("2024-01-07T23:59:59Z")]
        [JsonPropertyName("data_previsao_termino")]
        public string? EstimatedReturnDate { get; set; }

        [DefaultValue(1)]
        [JsonPropertyName("plano")]
        public int PlanId { get; set; }

        [DefaultValue("2024-01-07T23:59:59Z")]
        public DateTime? ReturnedToBaseDate { get; set; }
    }
}
