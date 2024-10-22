using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete
{
    public class MotorcycleDeleteValidator
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IMotocycleRepository _repositoryMotorcycle;
        public readonly IRentalRepository _rentalRepository;

        public MotorcycleDeleteValidator(
            IBaseInternalServices baseInternalServices,
            IMotocycleRepository repositoryMotorcycle,
            IRentalRepository rentalRepository)
        {
            _baseInternalServices = baseInternalServices;
            _repositoryMotorcycle = repositoryMotorcycle;
            _rentalRepository = rentalRepository;
        }

        public async Task<string?> BuilderDeleteValidator(string? idMotorcycle)
        {
            var message = new StringBuilder();

            if (string.IsNullOrEmpty(idMotorcycle))
            {
                message.AppendError(message, nameof(idMotorcycle));
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle);

                if (motorcycle == null)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
                }

                var motorcycleIsInUse = await _rentalRepository.FindByMotorcycleId(idMotorcycle);

                if (motorcycleIsInUse != null)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.Unavailable);
                }
            }

            return _baseInternalServices.BuildMessageValidator(message);
        }
    }
}
