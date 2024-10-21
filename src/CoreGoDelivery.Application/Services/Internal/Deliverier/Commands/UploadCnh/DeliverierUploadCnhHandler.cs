using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhHandler : DeliverierServiceBase, IRequestHandler<DeliverierUploadCnhCommand, ApiResponse>
    {
        public DeliverierUploadCnhHandler(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence,
            IBaseInternalServices baseInternalServices)
        : base(
            repositoryDeliverier,
            repositoryLicence,
            baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(DeliverierUploadCnhCommand request, CancellationToken cancellationToken)
        {
            // TODO: HEAVY MISSION IMAGE FILE
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return apiReponse;
        }
    }
}
