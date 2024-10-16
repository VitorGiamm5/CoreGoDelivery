using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motocycle
{
    public class MotocycleService : MotocycleServiceBase, IMotocycleService
    {
        private readonly IMotocycleRepository _repositoryMotorcycle;
        private readonly IModelMotocycleRepository _repositoryModelMotorcycle;
        private readonly IRentalRepository _rentalRepository;

        private const int NOTIFICATION_YEAR_MANUFACTORY = 2024;

        public MotocycleService(
            IMotocycleRepository repositoryMotocycle,
            IModelMotocycleRepository modelMotocycleRepository)
        {
            _repositoryMotorcycle = repositoryMotocycle;
            _repositoryModelMotorcycle = modelMotocycleRepository;
        }

        #region Public
        public async Task<ApiResponse> ChangePlate(string id, PlateIdDto plate)
        {
            var plateNormalized = RemoveCharacteres(plate?.Placa ?? "");

            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> Create(MotocycleDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await ValidatorCreateAsync(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var motocycle = CreateToEntity(data);

            var resultCreate = await _repositoryMotorcycle.Create(motocycle);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            await SendNotification(motocycle);

            return apiReponse;
        }

        public async Task<ApiResponse> GetOne(string id)
        {
            var result = await _repositoryMotorcycle.GetOneById(id);

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> List(string? plate)
        {
            string plateNormalized = RemoveCharacteres(plate);

            var result = await _repositoryMotorcycle.List(plateNormalized);

            var apiReponse = new ApiResponse()
            {
                Data = result?.Count != 0 ? result : null,
                Message = result?.Count == 0 ? "Dados inválidos" : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> Delete(string id)
        {
            var apiReponse = new ApiResponse()
            {
                Message = await ValidateDelete(id)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var result = await _repositoryMotorcycle.DeleteById(id);


            return apiReponse;
        }

        #endregion

        #region Private

        private async Task<string?> ValidateDelete(string id)
        {
            var message = new StringBuilder();

            var motorcycle = await _repositoryMotorcycle.GetOneById(id);
            if (motorcycle == null)
            {
                message.Append($"Invalid: {nameof(motorcycle)} id: {id} does not exist; ");
            }

            var motorcycleIsInUse = await _rentalRepository.FindByMotorcycleId(id);
            if (motorcycleIsInUse != null)
            {
                message.Append($"Invalid: {nameof(motorcycle)} id: {id} is in use; ");
            }

            return message.ToString();
        }

        private async Task<string?> ValidatorCreateAsync(MotocycleDto data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryMotorcycle.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.Append($"{nameof(data.Id)}: {data.Id} already exists; ");
                }
            }
            #endregion

            #region Plate validator
            if (string.IsNullOrWhiteSpace(data.PlateId))
            {
                message.Append($"Empty: {nameof(data.PlateId)}; ");
            }
            else
            {
                var isValidPlate = ValidatePlate(data.PlateId);
                if (isValidPlate)
                {
                    var normalizedPlate = RemoveCharacteres(data.PlateId);
                    var isUnicId = await _repositoryMotorcycle.CheckIsUnicByPlateId(normalizedPlate);

                    if (!isUnicId)
                    {
                        message.Append($"{nameof(data.PlateId)}: {data.PlateId} already exists; ");
                    }
                }
                else
                {
                    message.Append($"Invalid: {nameof(data.PlateId)}: {data.PlateId} format; ");
                }
            }
            #endregion

            #region Year validator
            if (string.IsNullOrWhiteSpace(data.YearManufacture.ToString()))
            {
                message.Append($"Empty: {nameof(data.YearManufacture)}; ");
            }
            else
            {
                if (data.YearManufacture <= 1903)
                {
                    message.Append($"Invalid: {nameof(data.YearManufacture)}: {data.YearManufacture}; ");
                }
            }
            #endregion

            #region Model validator
            if (string.IsNullOrWhiteSpace(data.ModelName))
            {
                message.Append($"Empty: {nameof(data.ModelName)}; ");
            }
            else
            {
                var modelNormalized = RemoveCharacteres(data.ModelName);
                var modelId = await GetModelId(modelNormalized);

                if (string.IsNullOrEmpty(modelId))
                {
                    message.Append($"Invalid: {nameof(data.ModelName)}: {data.ModelName} not exist; ");
                }

                data.ModelName = modelId;
            }
            #endregion

            #region Finaly
            if (message.Length > 0)
            {
                return message.ToString();
            }
            #endregion

            return null;
        }

        private async Task<string> GetModelId(string modelNormalized)
        {
            var result = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

            return result;
        }

        private async Task SendNotification(MotocycleEntity motocycle)
        {
            if (motocycle.YearManufacture == NOTIFICATION_YEAR_MANUFACTORY)
            {
                //TODO: HEAVY MISSION NOTIFICATION
            }
        }
        #endregion
    }
}
