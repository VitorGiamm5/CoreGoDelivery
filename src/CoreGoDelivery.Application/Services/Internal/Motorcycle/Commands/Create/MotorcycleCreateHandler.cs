using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public class MotorcycleCreateHandler : IRequestHandler<MotorcycleCreateCommand, ActionResult>
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

    public async Task<ActionResult> Handle(MotorcycleCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();
        apiReponse.SetMessage(await _validator.BuilderCreateValidator(request));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var motorcycle = _mapper.MapCreateToEntity(request);

        var resultCreate = await _repositoryMotorcycle.Create(motorcycle);

        apiReponse.SetMessage(_baseInternalServices.FinalMessageBuild(resultCreate, apiReponse));

        await _notification.SendNotification(motorcycle);

        return apiReponse;
    }
}
