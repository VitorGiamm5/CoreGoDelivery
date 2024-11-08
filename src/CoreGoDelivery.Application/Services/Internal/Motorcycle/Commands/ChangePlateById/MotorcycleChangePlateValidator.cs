using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Commons;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;

public class MotorcycleChangePlateValidator
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    private readonly MotorcycleCreateValidator _validatorCreate;

    public MotorcycleChangePlateValidator(
        IMotorcycleRepository repositoryMotorcycle,
        MotorcycleCreateValidator validatorCreate)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _validatorCreate = validatorCreate;
    }

    public async Task<StringBuilder> ChangePlateValidator(MotorcycleChangePlateCommand command)
    {
        var message = new StringBuilder();

        await _validatorCreate.BuildMessagePlate(command.Plate, message);

        await MessageBuildChangePlate(command.Plate, message);

        return message;
    }

    public async Task BuildMessageChangePlateId(string? idMotorcycle, StringBuilder message)
    {
        var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle!);

        if (motorcycle == null)
        {
            message.Append(nameof(idMotorcycle).AppendError(AdditionalMessageEnum.NotFound));
        }
    }

    public async Task MessageBuildChangePlate(string? plate, StringBuilder message)
    {
        if (string.IsNullOrEmpty(plate))
        {
            message.Append(nameof(plate));
        }
        else
        {
            var isValidPlate = MotorcyclePlateValidator.Validator(plate!);

            if (!isValidPlate)
            {
                message.Append(nameof(plate).AppendError(AdditionalMessageEnum.InvalidFormat));
            }
            else
            {
                var plateIsUnic = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(plate);

                if (!plateIsUnic)
                {
                    message.Append(nameof(plate).AppendError(AdditionalMessageEnum.MustBeUnic));
                }
            }
        }
    }
}
