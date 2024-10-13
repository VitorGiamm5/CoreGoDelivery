using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Response
{
    public class ApiResponse
    {
        public object? Data { get; set; } = null;

        [JsonPropertyName("mensagem")]
        public string? Error { get; set; }
    }
}
