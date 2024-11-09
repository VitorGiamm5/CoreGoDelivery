using CoreGoDelivery.Domain.Consts.Regex;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Commons;

public static class MotorcyclePlateValidator
{
    public static bool Validator(string? plateId)
    {
        if (plateId == null)
        {
            return false;
        }

        var plate = Regex.Replace(plateId, RegexCollectionPatterns.SPECIAL_CHARACTER_PATTERN, "").ToUpper();

        var platePatterIsValid = Regex.IsMatch(plate, $"{RegexCollectionPatterns.PLATE_FORMAT_OLD}|{RegexCollectionPatterns.PLATE_FORMAT_NEW}");

        return platePatterIsValid;
    }
}
