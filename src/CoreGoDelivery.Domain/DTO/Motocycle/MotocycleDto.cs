using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Motocycle
{
    public sealed class MotocycleDto
    {
        [JsonPropertyName("identificador")]
        public string MotocycleId { get; set; }

        [JsonPropertyName("ano")]
        public int YearManufacture { get; set; }

        [JsonPropertyName("modelo")]
        public string ModelName { get; set; }

        [JsonPropertyName("placa")]
        public string IdentificationPlate { get; set; }
    }
}
