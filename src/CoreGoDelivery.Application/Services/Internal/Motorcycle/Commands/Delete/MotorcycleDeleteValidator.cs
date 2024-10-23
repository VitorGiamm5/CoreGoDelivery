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
        public readonly IMotorcycleRepository _repositoryMotorcycle;
        public readonly IRentalRepository _rentalRepository;

        public MotorcycleDeleteValidator(
            IBaseInternalServices baseInternalServices,
            IMotorcycleRepository repositoryMotorcycle,
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

                return _baseInternalServices.BuildMessageValidator(message);
            }

            var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle);

            if (motorcycle == null)
            {
                message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);

                return _baseInternalServices.BuildMessageValidator(message);
            }

            var rental = await _rentalRepository.FindByMotorcycleId(idMotorcycle);

            if (rental == null)
            {
                return _baseInternalServices.BuildMessageValidator(message);
            }

            var motorcycleIsAvaliable = await _rentalRepository.CheckMotorcycleIsAvaliable(idMotorcycle);

            if (!motorcycleIsAvaliable)
            {
                message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.Unavailable);
            }

            return _baseInternalServices.BuildMessageValidator(message);
        }
    }
}
