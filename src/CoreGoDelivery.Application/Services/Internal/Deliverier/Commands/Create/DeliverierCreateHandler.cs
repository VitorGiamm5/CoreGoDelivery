using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateHandler : IRequestHandler<DeliverierCreateCommand, ApiResponse>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IDeliverierRepository _repositoryDeliverier;
    public readonly IMediator _mediator;
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
        _mediator = mediator;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeliverierCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ApiResponse()
        {
            Data = null,
            Message = await _validator.Validator(request)
        };

        if (_baseInternalServices.HasMessageError(apiReponse))
        {
            return apiReponse;
        }

        var deliverier = _mapper.MapCreateToEntity(request);

        var resultCreate = await _repositoryDeliverier.Create(deliverier);

        if (!resultCreate)
        {
            return apiReponse;
        }

        apiReponse.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

        if (apiReponse.HasError())
        {
            apiReponse.Data = null;
            return apiReponse;
        }

        var deliverierUpload = new LicenseImageCommand()
        {
            IdDeliverier = deliverier.Id,
            Id = deliverier.LicenceDriver.Id,
            LicenseImageBase64 = request.LicenseImageBase64,
            IsUpdate = false
        };

        apiReponse = await _mediator.Send(deliverierUpload);

        if (apiReponse.HasError())
        {
            apiReponse.Data = null;
            return apiReponse;
        }

        return apiReponse;
    }
}
