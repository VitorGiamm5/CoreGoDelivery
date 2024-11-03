using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public class MotorcycleCreateValidator
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;
    public readonly IModelMotorcycleRepository _repositoryModelMotorcycle;
    public readonly IBaseInternalServices _baseInternalServices;

    public MotorcycleCreateValidator(
        IMotorcycleRepository repositoryMotorcycle,
        IModelMotorcycleRepository repositoryModelMotorcycle,
        IBaseInternalServices baseInternalServices)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryModelMotorcycle = repositoryModelMotorcycle;
        _baseInternalServices = baseInternalServices;
    }

    public async Task<string?> BuilderCreateValidator(MotorcycleCreateCommand data)
    {
        var message = new StringBuilder();

        BuildMessageYear(data, message);

        await BuildMessageIdMotorcycle(data, message);
        await BuildMessagePlate(data.PlateId, message);
        await BuildMessageModelMotorcycle(data, message);

        return _baseInternalServices.BuildMessageValidator(message);
    }

    public void BuildMessageYear(MotorcycleCreateCommand data, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(data.YearManufacture.ToString()))
        {
            message.AppendError(message, nameof(data.YearManufacture));
        }
        else
        {
            if (data.YearManufacture <= 1903)
            {
                message.AppendError(message, nameof(data.YearManufacture), AdditionalMessageEnum.Unavailable);
            }
        }
    }

    public async Task BuildMessagePlate(string? plate, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(plate))
        {
            message.AppendError(message, nameof(plate));
        }
        else
        {
            var normalizedPlate = _baseInternalServices.RemoveCharacteres(plate);

            var isValidPlate = MotorcyclePlateValidator.Validator(normalizedPlate);

            if (isValidPlate)
            {
                var isUnicId = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(normalizedPlate);

                if (!isUnicId)
                {
                    message.AppendError(message, nameof(plate), AdditionalMessageEnum.AlreadyExist);
                }
            }
            else
            {
                message.AppendError(message, nameof(plate), AdditionalMessageEnum.InvalidFormat);
            }
        }
    }

    public async Task BuildMessageIdMotorcycle(MotorcycleCreateCommand data, StringBuilder message)
    {
        var idMotorcycle = data.Id;

        if (!string.IsNullOrWhiteSpace(idMotorcycle))
        {
            var isUnicId = await _repositoryMotorcycle.CheckIsUnicById(idMotorcycle);

            if (!isUnicId)
            {
                message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.AlreadyExist);
            }
        }
    }

    public async Task BuildMessageModelMotorcycle(MotorcycleCreateCommand data, StringBuilder message)
    {
        var idMotorcycle = data.Id;

        if (string.IsNullOrWhiteSpace(data.ModelName))
        {
            message.AppendError(message, nameof(data.ModelName));
        }
        else
        {
            var modelNormalized = _baseInternalServices.RemoveCharacteres(data.ModelName);

            var modelId = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

            if (string.IsNullOrEmpty(modelId))
            {
                message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
            }

            data.ModelName = modelId;
        }
    }
}
