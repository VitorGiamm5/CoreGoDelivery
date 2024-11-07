using CoreGoDelivery.Application.Services.Internal.Base;
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
    public readonly IBaseInternalServices _baseInternalServices;

    public readonly MotorcycleChangePlateValidator _validator;

    public MotorcycleChangePlateHandler(
        IMotorcycleRepository repositoryMotorcycle,
        IModelMotorcycleRepository repositoryModelMotorcycle,
        IRentalRepository rentalRepository,
        IBaseInternalServices baseInternalServices,
        MotorcycleChangePlateValidator validator)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _repositoryModelMotorcycle = repositoryModelMotorcycle;
        _rentalRepository = rentalRepository;
        _baseInternalServices = baseInternalServices;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(MotorcycleChangePlateCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetMessage(await _validator.ChangePlateValidator(command));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var plateNormalized = _baseInternalServices.RemoveCharacteres(command.Plate);

        var success = await _repositoryMotorcycle.ChangePlateByIdAsync(command.Id, plateNormalized);

        apiReponse.Data = success
            ? new
            {
                menssage = CommomMessagesConst.MESSAGE_UPDATED_WITH_SUCCESS
            }
            : null;

        apiReponse.SetMessage(success ? null : CommomMessagesConst.MESSAGE_INVALID_DATA);

        return apiReponse;
    }
}
