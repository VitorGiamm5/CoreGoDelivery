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

    public static string RemoveCharacters(this string? plateId)
    {
        if (string.IsNullOrEmpty(plateId))
        {
            return string.Empty;
        }

        var result = Regex.Replace(plateId, RegexCollectionPatterns.SPECIAL_CHARACTER_PATTERN, "").ToUpper();

        return result;
    }
}
