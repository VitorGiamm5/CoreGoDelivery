using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Deliverier
{
    public class LicenseImageString
    {
        [DefaultValue("base64string")]
        [JsonPropertyName("imagem_cnh")]
        public string LicenseImageBase64 { get; set; }
    }
}
