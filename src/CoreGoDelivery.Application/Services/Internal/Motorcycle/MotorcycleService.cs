using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motorcycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle
{
    public class MotorcycleService : MotorcycleServiceBase, IMotocycleService
    {
        public MotorcycleService(
            IMotocycleRepository repositoryMotorcycle, 
            IModelMotocycleRepository repositoryModelMotorcycle, 
            IRentalRepository rentalRepository) 
            : base(repositoryMotorcycle, repositoryModelMotorcycle, rentalRepository)
        {
        }

        public async Task<ApiResponse> ChangePlateById(string? id, string? plate)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await ChangePlateValidator(id, plate)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plateNormalized = RemoveCharacteres(plate);

            var success = await _repositoryMotorcycle.ChangePlateByIdAsync(id, plateNormalized);

            apiReponse.Data = success ? new { mensagem = "Placa modificada com sucesso" } : null;
            apiReponse.Message = success ? null : MESSAGE_INVALID_DATA;

            return apiReponse;
        }

        public async Task<ApiResponse> Create(MotorcycleDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderCreateValidator(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var motocycle = MapCreateToEntity(data);

            var resultCreate = await _repositoryMotorcycle.Create(motocycle);

            apiReponse!.Message = FinalMessageBuild(resultCreate, apiReponse);

            await SendNotification(motocycle);

            return apiReponse;
        }

        public async Task<ApiResponse> GetOne(string id)
        {
            var result = await _repositoryMotorcycle.GetOneByIdAsync(id);

            var motocycleDtos = result != null ? MapEntityToDto(result) : null;

            var apiReponse = new ApiResponse()
            {
                Data = motocycleDtos,
                Message = result == null ? "Moto não encontrada" : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> List(string? plate)
        {
            string plateNormalized = RemoveCharacteres(plate);

            var result = await _repositoryMotorcycle.List(plateNormalized);

            var motocycleDtos = MapEntityListToDto(result);

            var apiReponse = new ApiResponse()
            {
                Data = motocycleDtos?.Count != 0 ? motocycleDtos : null,
                Message = result?.Count == 0 ? MESSAGE_INVALID_DATA : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> DeleteById(string? id)
        {
            var apiReponse = new ApiResponse()
            {
                Message = await BuilderDeleteValidator(id)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            _ = await _repositoryMotorcycle.DeleteById(id);

            return apiReponse!;
        }
    }
}
