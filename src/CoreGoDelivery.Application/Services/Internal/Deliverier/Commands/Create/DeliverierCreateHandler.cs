using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create
{
    public class DeliverierCreateHandler : DeliverierServiceBase, IRequestHandler<DeliverierCreateCommand, ApiResponse>
    {
        public DeliverierCreateHandler(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence,
            IBaseInternalServices baseInternalServices)
        : base(
            repositoryDeliverier,
            repositoryLicence,
            baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(DeliverierCreateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderValidatorCreateAsync(request)
            };

            if (_baseInternalServices.HasMessageError(apiReponse))
            {
                return apiReponse;
            }

            var deliverier = MapCreateToEntity(request);

            var resultCreate = await _repositoryDeliverier.Create(deliverier);

            apiReponse.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }
    }
}
