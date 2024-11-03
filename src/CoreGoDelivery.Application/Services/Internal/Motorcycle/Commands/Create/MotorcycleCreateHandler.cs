using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public class MotorcycleCreateHandler : IRequestHandler<MotorcycleCreateCommand, ApiResponse>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    private readonly MotorcycleCreateValidator _validator;
    private readonly MotorcycleServiceMappers _mapper;
    private readonly MotorcycleCreateNotification _notification;

    public MotorcycleCreateHandler(
        IBaseInternalServices baseInternalServices,
        IMotorcycleRepository repositoryMotorcycle,
        MotorcycleCreateValidator validator,
        MotorcycleServiceMappers mapper,
        MotorcycleCreateNotification notification)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryMotorcycle = repositoryMotorcycle;
        _validator = validator;
        _mapper = mapper;
        _notification = notification;
    }

    public async Task<ApiResponse> Handle(MotorcycleCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ApiResponse()
        {
            Data = null,
            Message = await _validator.BuilderCreateValidator(request)
        };

        if (!string.IsNullOrEmpty(apiReponse.Message))
        {
            return apiReponse;
        }

        var motorcycle = _mapper.MapCreateToEntity(request);

        var resultCreate = await _repositoryMotorcycle.Create(motorcycle);

        apiReponse!.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

        await _notification.SendNotification(motorcycle);

        return apiReponse;
    }
}
