using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Rental
{
    public sealed class ResponseMessageDto
    {
        [JsonPropertyName("mensagem")]
        public string Message { get; set; }
    }
}
