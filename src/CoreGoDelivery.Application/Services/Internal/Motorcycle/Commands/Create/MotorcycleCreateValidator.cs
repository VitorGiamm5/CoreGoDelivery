using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Commons;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public class MotorcycleCreateValidator
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;
    public readonly IModelMotorcycleRepository _repositoryModelMotorcycle;

    public MotorcycleCreateValidator(
        IMotorcycleRepository repositoryMotorcycle,
        IModelMotorcycleRepository repositoryModelMotorcycle)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryModelMotorcycle = repositoryModelMotorcycle;
    }

    public async Task<StringBuilder> Validate(MotorcycleCreateCommand data)
    {
        var message = new StringBuilder();

        BuildMessageYear(data, message);

        await BuildMessagePlate(data.Plate, message);
        await BuildMessageModelMotorcycle(data, message);

        return message;
    }

    public static void BuildMessageYear(MotorcycleCreateCommand data, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(data.YearManufacture.ToString()))
        {
            message.Append(nameof(data.YearManufacture));
        }
        else
        {
            if (data.YearManufacture <= 1903)
            {
                message.Append(nameof(data.YearManufacture).AppendError(AdditionalMessageEnum.Unavailable));
            }
        }
    }

    public async Task BuildMessagePlate(string? plate, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(plate))
        {
            message.Append(nameof(plate));
        }
        else
        {
            var isValidPlate = MotorcyclePlateValidator.Validator(plate);

            if (isValidPlate)
            {
                var isUnicId = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(plate);

                if (!isUnicId)
                {
                    message.Append(nameof(plate).AppendError(AdditionalMessageEnum.AlreadyExist));
                }
            }
            else
            {
                message.Append(nameof(plate).AppendError(AdditionalMessageEnum.InvalidFormat));
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
                message.Append(nameof(idMotorcycle).AppendError(AdditionalMessageEnum.AlreadyExist));
            }
        }
    }

    public async Task BuildMessageModelMotorcycle(MotorcycleCreateCommand data, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(data.ModelName))
        {
            message.Append(nameof(data.ModelName));
        }
        else
        {
            var modelNormalized = data.ModelName.RemoveCharactersToUpper();

            var modelId = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

            if (string.IsNullOrEmpty(modelId))
            {
                message.Append("idMotorcycle".AppendError(AdditionalMessageEnum.NotFound));
            }

            data.ModelName = modelId;
        }
    }
}
