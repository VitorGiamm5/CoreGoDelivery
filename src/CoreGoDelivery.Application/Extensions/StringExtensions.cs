using CoreGoDelivery.Domain.Consts.Regex;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Extensions;

public static class StringExtensions
{
    public static string AppendError(this string fieldName, AdditionalMessageEnum additionalMessage = AdditionalMessageEnum.Required)
    {
        return $"Invalid field: '{fieldName}', Detail: '{additionalMessage.GetMessage()}'; ";
    }

    public static string RemoveCharactersToUpper(this string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        var result = Regex.Replace(text, RegexCollectionPatterns.SPECIAL_CHARACTER_PATTERN, "").ToUpper();

        return result;
    }
}
