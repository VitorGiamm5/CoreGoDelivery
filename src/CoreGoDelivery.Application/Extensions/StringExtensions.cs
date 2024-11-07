using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;

namespace CoreGoDelivery.Application.Extensions;

public static class StringExtensions
{
    public static string AppendError(this string fieldName, AdditionalMessageEnum additionalMessage = AdditionalMessageEnum.Required)
    {
        return $"Invalid field: '{fieldName}', Detail: '{additionalMessage.GetMessage()}'; ";
    }
}
