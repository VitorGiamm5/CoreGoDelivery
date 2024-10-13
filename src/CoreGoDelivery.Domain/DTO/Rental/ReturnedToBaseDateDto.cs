using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Rental
{
    public class ReturnedToBaseDateDto
    {
        [DefaultValue("2024-01-07T23:59:59Z")]
        [JsonPropertyName("data_devolucao")]
        public DateTime? ReturnedToBaseDate { get; set; }
    }
}
