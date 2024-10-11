using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Deliverier
{
    public class DeliverierDto
    {
        [JsonPropertyName("identificador")]
        public string DeliverierId { get; set; }

        [JsonPropertyName("nome")]
        public string FullName { get; set; }

        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string LicenseNumber { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public string LicenseType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string LicenseImageBase64 { get; set; }
    }
}
