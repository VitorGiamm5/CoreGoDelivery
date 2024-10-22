using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons
{
    public class PlateValidator
    {
        public static bool Validator(string? plateId)
        {
            if (plateId == null)
            {
                return false;
            }

            var plate = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            if (Regex.IsMatch(plate, @"^[A-Z]{3}\d{4}$") ||
                Regex.IsMatch(plate, @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
