using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateHandler : IRequestHandler<DeliverierCreateCommand, ActionResult>
{
    public readonly IDeliverierRepository _repositoryDeliverier;
    public readonly DeliverierCreateValidator _validator;

    public DeliverierCreateHandler(
        IDeliverierRepository repositoryDeliverier,
        DeliverierCreateValidator validator)
    {
        _repositoryDeliverier = repositoryDeliverier;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(DeliverierCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetMessage(await _validator.Validator(request));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var deliverier = DeliverierCreateMappers.MapCreateToEntity(request);

        var resultCreate = await _repositoryDeliverier.Create(deliverier);

        if (!resultCreate)
        {
            apiReponse.SetMessage(nameof(resultCreate).AppendError(AdditionalMessageEnum.Unavailable));

            return apiReponse;
        }

        return apiReponse;
    }
}
