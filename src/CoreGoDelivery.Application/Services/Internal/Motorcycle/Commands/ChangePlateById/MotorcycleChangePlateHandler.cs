using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;

public class MotorcycleChangePlateHandler : IRequestHandler<MotorcycleChangePlateCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;
    public readonly IMotorcycleModelRepository _repositoryMotorcycleModel;
    public readonly IRentalRepository _rentalRepository;

    public readonly MotorcycleChangePlateValidator _validator;

    public MotorcycleChangePlateHandler(
        IMotorcycleRepository repositoryMotorcycle,
        IMotorcycleModelRepository repositoryMotorcycleModel,
        IRentalRepository rentalRepository,
        MotorcycleChangePlateValidator validator)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryMotorcycleModel = repositoryMotorcycleModel;
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

        var success = await _repositoryMotorcycle.ChangePlateByIdAsync(command.Id!, command.Plate!);

        apiReponse.SetData(new
        {
            menssage = CommomMessagesConst.MESSAGE_UPDATED_WITH_SUCCESS
        });

        apiReponse.SetError(success ? "" : CommomMessagesConst.MESSAGE_INVALID_DATA);

        return apiReponse;
    }
}
