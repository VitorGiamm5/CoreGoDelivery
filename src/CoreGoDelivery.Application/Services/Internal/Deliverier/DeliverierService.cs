using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierService : DeliverierServiceBase, IDeliverierService
    {
        public DeliverierService(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence)
            : base(repositoryDeliverier, repositoryLicence)
        {
        }

        public async Task<ApiResponse> Create(DeliverierDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderValidatorCreateAsync(data)
            };
            
            if (HasMessageError(apiReponse))
            {
                return apiReponse;
            }

            var deliverier = CreateToEntity(data);

            var resultCreate = await _repositoryDeliverier.Create(deliverier);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        public async Task<ApiResponse> UploadCnh(string id)
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
