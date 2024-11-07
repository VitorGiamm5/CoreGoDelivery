using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;

public class MotorcycleCreateHandler : IRequestHandler<MotorcycleCreateCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    private readonly MotorcycleCreateValidator _validator;
    private readonly MotorcycleCreateNotification _notification;

    public MotorcycleCreateHandler(
        IMotorcycleRepository repositoryMotorcycle,
        MotorcycleCreateValidator validator,
        MotorcycleCreateNotification notification)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _validator = validator;
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

        var motorcycle = MotorcycleServiceMappers.MapCreateToEntity(request);

        var isSuccess = await _repositoryMotorcycle.Create(motorcycle);

        if (!isSuccess)
        {
            apiReponse.SetMessage(nameof(_repositoryMotorcycle.Create).AppendError(AdditionalMessageEnum.CreateFail));
        }

        await _notification.SendNotification(motorcycle);

        return apiReponse;
    }
}
