using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motocycle
{
    public class MotocycleServiceBase
    {
        public const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public static MotorcycleEntity CreateToEntity(MotocycleDto data)
        {
            return new MotorcycleEntity()
            {
                Id = SelectId(data),
                YearManufacture = data.YearManufacture,
                ModelMotorcycleId = data.ModelName,
                PlateNormalized = RemoveCharacteres(data.PlateId)
            };
        }

        public static string RemoveCharacteres(string? plateId)
        {
            var result = !string.IsNullOrEmpty(plateId)
                ? Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper()
                : "";

            return result;
        }

        public static string SelectId(MotocycleDto data)
        {
            var result = string.IsNullOrEmpty(data.Id) ? Ulid.NewUlid().ToString() : data.Id;

            return result;
        }

        public static bool ValidatePlate(string plateId)
        {
            var plate = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            if (Regex.IsMatch(plate, @"^[A-Z]{3}\d{4}$") ||    // old format
                Regex.IsMatch(plate, @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$")) // new format
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }
    }
}