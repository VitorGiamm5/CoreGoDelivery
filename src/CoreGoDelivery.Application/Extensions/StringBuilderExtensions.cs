using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text;

namespace CoreGoDelivery.Application.Extensions;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendError<T>(this StringBuilder sb, T data, object? paramName, AdditionalMessageEnum additionalMessage = AdditionalMessageEnum.Required)

    {
        if (paramName == null)
        {
            sb.Append($"Invalid field: '{paramName}', Detail: '{additionalMessage.GetMessage()}'; ");
        }
        else
        {
            sb.Append($"Invalid field: '{paramName}', Detail: '{additionalMessage.GetMessage()}'; ");
        }

        return sb;
    }

    public static void AppendErrorWithExpexted<T>(this StringBuilder message, T data, object? wantedValue, string butValue)
    {
        string result = $"" +
            $"Invalid field: {wantedValue}, " +
            $"Expected: {wantedValue}, " +
            $"Type: {typeof(T).Name}, " +
            $"But was: {butValue};";

        message.Append(result);
    }
}
