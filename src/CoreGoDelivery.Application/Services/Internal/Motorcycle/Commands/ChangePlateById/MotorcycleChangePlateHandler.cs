using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;

public class MotorcycleChangePlateHandler : IRequestHandler<MotorcycleChangePlateCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;
    public readonly IModelMotorcycleRepository _repositoryModelMotorcycle;
    public readonly IRentalRepository _rentalRepository;

    public readonly MotorcycleChangePlateValidator _validator;

    public MotorcycleChangePlateHandler(
        IMotorcycleRepository repositoryMotorcycle,
        IModelMotorcycleRepository repositoryModelMotorcycle,
        IRentalRepository rentalRepository,
        MotorcycleChangePlateValidator validator)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryModelMotorcycle = repositoryModelMotorcycle;
        _rentalRepository = rentalRepository;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(MotorcycleChangePlateCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetError(await _validator.ChangePlateValidator(command));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        command.Plate.RemoveCharactersToUpper();

        var success = await _repositoryMotorcycle.ChangePlateByIdAsync(command.Id, command.Plate);

        apiReponse.SetData(success
            ? new
            {
                menssage = CommomMessagesConst.MESSAGE_UPDATED_WITH_SUCCESS
            }
            : null);

        apiReponse.SetError(success ? null : CommomMessagesConst.MESSAGE_INVALID_DATA);

        return apiReponse;
    }
}
