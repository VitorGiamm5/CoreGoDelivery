using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Rental
{
    public sealed class RentalDto : ReturnedToBaseDateDto
    {
        [DefaultValue("locacao123")]
        public string RentalId { get; set; }

        [DefaultValue(10)]
        [JsonPropertyName("valor_diaria")]
        public decimal DayliRate { get; set; }

        [DefaultValue("entregador123")]
        [JsonPropertyName("entregador_id")]
        public string DeliveryPersonId { get; set; }

        [DefaultValue("moto123")]
        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [DefaultValue("2024-01-01T00:00:00Z")]
        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [DefaultValue("2024-01-07T23:59:59Z")]
        [JsonPropertyName("data_termino")]
        public DateTime? EndDate { get; set; }

        [DefaultValue("2024-01-07T23:59:59Z")]
        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }

        [DefaultValue(1)]
        [JsonPropertyName("plano")]
        public int PlanId { get; set; }
    }
}
