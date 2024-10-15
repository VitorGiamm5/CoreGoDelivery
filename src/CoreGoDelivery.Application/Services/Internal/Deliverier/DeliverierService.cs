using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Repositories.GoDelivery;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierService : IDeliverierService
    {
        private readonly IDeliverierRepository _repositoryDeliverier;
        private readonly ILicenceDriverRepository _repositoryLicence;

        public DeliverierService(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence)
        {
            _repositoryDeliverier = repositoryDeliverier;
            _repositoryLicence = repositoryLicence;
        }

        public async Task<ApiResponse> Create(DeliverierDto data)
        {


            var deliverier = new DeliverierEntity()
            {
                Id = Ulid.NewUlid().ToString(),
                FullName = data.FullName,
                CNPJ = data.CNPJ,
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = Ulid.NewUlid().ToString(),
                    Type = data.LicenseType,
                    FileNameImageNormalized = FileNameNormalize(data)
                }
            };

            var resultDeliverier = await _repositoryDeliverier.Create(deliverier);

            var apiReponse = new ApiResponse()
            {
                Data = deliverier,
                Message = null
            };

            return apiReponse;
        }

        private static string FileNameNormalize(DeliverierDto data)
        {
            return $"CNPJ_{data.CNPJ}";
        }

        public async Task<ApiResponse> UploadCnh(string id)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return apiReponse;
        }
    }
}
