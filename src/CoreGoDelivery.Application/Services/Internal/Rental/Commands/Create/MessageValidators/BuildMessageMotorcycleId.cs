using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators
{
    public class BuildMessageMotorcycleId
    {
        public readonly IMotorcycleRepository _repositoryMotorcycle;
        public readonly IRentalRepository _repositoryRental;

        public BuildMessageMotorcycleId(IMotorcycleRepository repositoryMotorcycle, IRentalRepository repositoryRental)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _repositoryRental = repositoryRental;
        }

        public async Task Build(RentalCreateCommand data, StringBuilder message, string paramName)
        {
            if (string.IsNullOrWhiteSpace(data.MotorcycleId))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                var existMotorcycleId = await _repositoryMotorcycle.GetOneByIdAsync(data.MotorcycleId);

                if (existMotorcycleId == null)
                {
                    message.AppendError(message, nameof(data.MotorcycleId), AdditionalMessageEnum.NotFound);
                }

                var motorcycleIsInUse = await _repositoryRental.CheckMotorcycleIsAvaliable(data.MotorcycleId);

                if (motorcycleIsInUse)
                {
                    message.AppendError(message, nameof(data.MotorcycleId), AdditionalMessageEnum.Unavailable);
                }
            }
        }
    }
}
