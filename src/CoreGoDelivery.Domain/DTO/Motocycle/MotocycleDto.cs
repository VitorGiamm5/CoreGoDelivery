using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Motocycle
{
    public sealed class MotocycleDto
    {
        [Key]
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
