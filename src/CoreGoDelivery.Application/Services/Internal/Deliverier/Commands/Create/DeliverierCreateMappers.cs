using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public static class DeliverierCreateMappers
{
    public static DeliverierEntity MapCreateToEntity(DeliverierCreateCommand command)
    {
        var result = new DeliverierEntity()
        {
            Id = command.Id,
            FullName = command.FullName,
            Cnpj = command.Cnpj.RemoveCharacters(),
            BirthDate = command.BirthDate,
            LicenceDriver = new LicenceDriverEntity()
            {
                Id = command.LicenseNumber,
                Type = DeliverierParseLicenseType.Parse(command),
                ImageUrlReference = "pending"
            }
        };

        return result;
    }
}
