using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhHandler : IRequestHandler<DeliverierUploadCnhCommand, ApiResponse>
    {
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
