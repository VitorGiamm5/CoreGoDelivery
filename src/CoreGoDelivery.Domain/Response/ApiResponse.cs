using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.Response;

public sealed class ApiResponse
{
    public object? Data { get; set; } = null;

    [JsonPropertyName("mensagem")]
    public string? Message { get; set; } = null!;

    public bool HasError()
    {
        return !string.IsNullOrWhiteSpace(Message);
    }

    public bool HasDataType()
    {
        return Data != null && !string.IsNullOrEmpty(Data.GetType().ToString());
    }
}
