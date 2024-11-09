using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.RabbitMQ.NotificationMotorcycle;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Commons;

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
            Plate = motorcycle.PlateNormalized
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
            PlateNormalized = data.Plate.RemoveCharactersToUpper()
        };

        return result;
    }

    public static NotificationMotorcycleDto MapNotificationDto(MotorcycleEntity data)
    {
        var result = new NotificationMotorcycleDto()
        {
            Id = data.Id,
            YearManufacture = data.YearManufacture,
            ModelMotorcycleId = data.ModelMotorcycleId,
            MotorcycleId = data.Id,
            PlateNormalized = data.PlateNormalized,
            DateCreated = data.DateCreated
        };

        return result;
    }
}
