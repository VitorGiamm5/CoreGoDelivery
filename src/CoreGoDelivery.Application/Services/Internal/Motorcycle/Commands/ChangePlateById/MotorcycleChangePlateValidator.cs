using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById
{
    public class MotorcycleChangePlateValidator
    {
        public readonly IMotocycleRepository _repositoryMotorcycle;
        public readonly IBaseInternalServices _baseInternalServices;

        private readonly MotorcycleCreateValidator _validatorCreate;

        public MotorcycleChangePlateValidator(
            IMotocycleRepository repositoryMotorcycle, 
            IBaseInternalServices baseInternalServices, 
            MotorcycleCreateValidator validatorCreate)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _baseInternalServices = baseInternalServices;
            _validatorCreate = validatorCreate;
        }

        public async Task<string?> ChangePlateValidator(string? id, string? plate)
        {
            var message = new StringBuilder();

            await _validatorCreate.BuildMessagePlate(id, message);

            await MessageBuildChangePlate(plate, message);

            return _baseInternalServices.BuildMessageValidator(message);
        }

        public async Task BuildMessageChangePlateId(string? idMotorcycle, StringBuilder message)
        {
            var isValidId = _baseInternalServices.RequestIdParamValidator(idMotorcycle);

            if (!isValidId)
            {
                message.AppendError(message, nameof(idMotorcycle));
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle!);

                if (motorcycle == null)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
                }
            }
        }

        public async Task MessageBuildChangePlate(string? plate, StringBuilder message)
        {
            if (string.IsNullOrEmpty(plate))
            {
                message.AppendError(message, nameof(plate));
            }
            else
            {
                var isValidPlate = PlateValidator.Validator(plate!);

                if (!isValidPlate)
                {
                    message.AppendError(message, nameof(plate), AdditionalMessageEnum.InvalidFormat);
                }
                else
                {
                    var plateIsUnic = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(plate);

                    if (!plateIsUnic)
                    {
                        message.AppendError(message, nameof(plate), AdditionalMessageEnum.MustBeUnic);
                    }
                }
            }
        }

    }
}
