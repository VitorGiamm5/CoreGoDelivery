using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Api.Extensions
{
    public class TrimStringJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value?.Trim();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            // Se desejar aplicar .Trim() também na serialização, descomente a linha abaixo
            // writer.WriteStringValue(value?.Trim());

            // Caso contrário, mantenha o valor sem alteração
            writer.WriteStringValue(value);
        }
    }
}
