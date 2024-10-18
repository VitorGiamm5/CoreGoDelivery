using CoreGoDelivery.Domain.DTO.Motorcycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle
{
    public class MotorcycleServiceBase : BaseInternalServices
    {
        public static MotorcycleEntity CreateToEntity(MotorcycleDto data)
        {
            var result = new MotorcycleEntity()
            {
                Id = IdBuild(data.Id),
                YearManufacture = data.YearManufacture,
                ModelMotorcycleId = data.ModelName,
                PlateNormalized = RemoveCharacteres(data.PlateId)
            };

            return result;
        }

        public static List<MotorcycleDto> EntityListToDto(List<MotorcycleEntity>? entity)
        {
            List<MotorcycleDto> motocycleDtos = [];

            if (entity != null)
            {
               var resultDto = motocycleDtos = entity
                    .Select(motorcycle => EntityToDto(motorcycle))
                    .ToList();

                return resultDto;
            }

            return new List<MotorcycleDto>();
        }

        public static MotorcycleDto EntityToDto(MotorcycleEntity motorcycle)
        {
            var restult = new MotorcycleDto
            {
                Id = motorcycle.Id,
                YearManufacture = motorcycle.YearManufacture,
                ModelName = motorcycle.ModelMotorcycle.Name,
                PlateId = motorcycle.PlateNormalized
            };

            return restult;
        }

        public static bool PlateValidator(string? plateId)
        {
            if (plateId == null)
            {
                return false;
            }

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
    }
}