using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;

public static class MotorcycleServiceMappers
{
    public static List<MotorcycleCreateCommand> MapEntityListToDto(List<MotorcycleEntity>? entity)
    {
        List<MotorcycleCreateCommand> motorcycleDtos = [];

        if (entity != null)
        {
            motorcycleDtos = entity
                 .Select(motorcycle => MapEntityToDto(motorcycle))
                 .ToList();

            return motorcycleDtos;
        }

        return motorcycleDtos;
    }

    public static MotorcycleCreateCommand MapEntityToDto(MotorcycleEntity motorcycle)
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

    public static MotorcycleEntity MapCreateToEntity(MotorcycleCreateCommand data)
    {
        var result = new MotorcycleEntity()
        {
            Id = data.Id,
            YearManufacture = data.YearManufacture,
            ModelMotorcycleId = data.ModelName,
            PlateNormalized = data.PlateId.RemoveCharacters()
        };

        return result;
    }
}
