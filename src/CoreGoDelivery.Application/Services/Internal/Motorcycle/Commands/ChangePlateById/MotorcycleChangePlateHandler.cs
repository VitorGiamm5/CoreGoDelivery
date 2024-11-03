using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;

public class MotorcycleChangePlateHandler : IRequestHandler<MotorcycleChangePlateCommand, ApiResponse>
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

    public async Task<ApiResponse> Handle(MotorcycleChangePlateCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ApiResponse()
        {
            Message = await _validator.ChangePlateValidator(command)
        };

        if (!string.IsNullOrEmpty(apiReponse.Message))
        {
            return apiReponse;
        }

        var plateNormalized = _baseInternalServices.RemoveCharacteres(command.Plate);

        var success = await _repositoryMotorcycle.ChangePlateByIdAsync(command.Id, plateNormalized);

        apiReponse.Data = success ? new { mensagem = CommomMessagesConst.MESSAGE_UPDATED_PLATE_SUCCESS } : null;

        apiReponse.Message = success ? null : CommomMessagesConst.MESSAGE_INVALID_DATA;

        return apiReponse;
    }
}
