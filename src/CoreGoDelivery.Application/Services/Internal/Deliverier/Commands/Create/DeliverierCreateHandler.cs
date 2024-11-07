using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateHandler : IRequestHandler<DeliverierCreateCommand, ActionResult>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IDeliverierRepository _repositoryDeliverier;
    public readonly DeliverierCreateValidator _validator;
    public readonly DeliverierCreateMappers _mapper;

    public DeliverierCreateHandler(
        IBaseInternalServices baseInternalServices,
        IDeliverierRepository repositoryDeliverier,
        IMediator mediator,
        DeliverierCreateValidator validator,
        DeliverierCreateMappers mapper
        )
    {
        _baseInternalServices = baseInternalServices;
        _repositoryDeliverier = repositoryDeliverier;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ActionResult> Handle(DeliverierCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetMessage(await _validator.Validator(request));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var deliverier = _mapper.MapCreateToEntity(request);

        var resultCreate = await _repositoryDeliverier.Create(deliverier);

        if (!resultCreate)
        {
            apiReponse.SetMessage(nameof(resultCreate).AppendError(AdditionalMessageEnum.Unavailable));
            return apiReponse;
        }

        apiReponse.SetMessage(_baseInternalServices.FinalMessageBuild(resultCreate, apiReponse));

        if (apiReponse.HasError())
        {
            apiReponse.Data = null;
            return apiReponse;
        }

        return apiReponse;
    }
}
