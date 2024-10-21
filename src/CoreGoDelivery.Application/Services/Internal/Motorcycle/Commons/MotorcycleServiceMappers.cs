using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons
{
    public class MotorcycleServiceMappers
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public MotorcycleServiceMappers(IBaseInternalServices baseInternalServices)
        {
            _baseInternalServices = baseInternalServices;
        }

        public List<MotorcycleCreateCommand> MapEntityListToDto(List<MotorcycleEntity>? entity)
        {
            List<MotorcycleCreateCommand> motocycleDtos = [];

            if (entity != null)
            {
                var resultDto = motocycleDtos = entity
                     .Select(motorcycle => MapEntityToDto(motorcycle))
                     .ToList();

                return resultDto;
            }

            return new List<MotorcycleCreateCommand>();
        }

        public MotorcycleCreateCommand MapEntityToDto(MotorcycleEntity motorcycle)
        {
            var restult = new MotorcycleCreateCommand
            {
                Id = motorcycle.Id,
                YearManufacture = motorcycle.YearManufacture,
                ModelName = motorcycle!.ModelMotorcycle!.Name,
                PlateId = motorcycle.PlateNormalized
            };

            return restult;
        }

        public MotorcycleEntity MapCreateToEntity(MotorcycleCreateCommand data)
        {
            var result = new MotorcycleEntity()
            {
                Id = _baseInternalServices.IdBuild(data.Id),
                YearManufacture = data.YearManufacture,
                ModelMotorcycleId = data.ModelName,
                PlateNormalized = _baseInternalServices.RemoveCharacteres(data.PlateId)
            };

            return result;
        }
    }
}
