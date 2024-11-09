using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;

public class RentalBuildMessageMotorcycleId
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;
    public readonly IRentalRepository _repositoryRental;

    public RentalBuildMessageMotorcycleId(IMotorcycleRepository repositoryMotorcycle, IRentalRepository repositoryRental)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryRental = repositoryRental;
    }

    public async Task Build(RentalCreateCommand data, StringBuilder message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(data.MotorcycleId))
        {
            message.Append(paramName);
        }
        else
        {
            var existMotorcycleId = await _repositoryMotorcycle.GetOneByIdAsync(data.MotorcycleId);

            if (existMotorcycleId == null)
            {
                message.Append(nameof(data.MotorcycleId).AppendError(AdditionalMessageEnum.NotFound));
            }

            var motorcycleIsInUse = await _repositoryRental.CheckMotorcycleIsAvaliable(data.MotorcycleId);

            if (motorcycleIsInUse)
            {
                message.Append(nameof(data.MotorcycleId).AppendError(AdditionalMessageEnum.Unavailable));
            }
        }
    }
}
