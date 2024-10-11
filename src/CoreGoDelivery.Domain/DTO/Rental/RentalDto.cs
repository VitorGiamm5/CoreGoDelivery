using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Rental
{
    public sealed class RentalDto
    {
        [JsonIgnore]
        public string RentalId { get; set; }

        [JsonPropertyName("entregador_id")]
        public string DeliveryPersonId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int PlanId { get; set; }
    }
}
