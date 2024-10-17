using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motorcycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle
{
    public class MotorcycleService : MotorcycleServiceBase, IMotocycleService
    {
        private readonly IMotocycleRepository _repositoryMotorcycle;
        private readonly IModelMotocycleRepository _repositoryModelMotorcycle;
        private readonly IRentalRepository _rentalRepository;

        public const int NOTIFICATION_YEAR_MANUFACTORY = 2024;

        public MotorcycleService(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _repositoryModelMotorcycle = repositoryModelMotorcycle;
            _rentalRepository = rentalRepository;
        }

        #region Public
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
            apiReponse.Message = success ? null : "Dados inválidos";

            return apiReponse;
        }

        public async Task<string?> ChangePlateValidator(string? id, string? plate)
        {
            var message = new StringBuilder();

            #region Id validator
            if (id == ":id" || string.IsNullOrEmpty(id))
            {
                message.Append($"Empty: {nameof(id)}; ");
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(id);
                if (motorcycle == null)
                {
                    message.Append($"Invalid: {nameof(id)} : {id} not exists; ");
                }
            }
            #endregion

            #region Plate validator
            if (string.IsNullOrEmpty(plate))
            {
                message.Append($"Empty: {nameof(plate)}; ");
            }
            else
            {
                var isValidPlate = ValidatePlate(plate!);
                if (!isValidPlate)
                {
                    message.Append($"Invalid: {nameof(plate)}: {plate} format; ");
                }
                else
                {
                    var plateIsUnic = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(plate);
                    if (!plateIsUnic)
                    {
                        message.Append($"Invalid: {nameof(plate)}: {plate} must be unic; ");
                    }
                }
            }
            #endregion

            return message.ToString();
        }

        public async Task<ApiResponse> Create(MotorcycleDto data)
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

            apiReponse!.Message = FinalMessageBuild(resultCreate, apiReponse);

            await SendNotification(motocycle);

            return apiReponse;
        }

        public async Task<ApiResponse> GetOne(string id)
        {
            var result = await _repositoryMotorcycle.GetOneByIdAsync(id);

            var motocycleDtos = result != null ? EntityToDto(result) : null;

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

            var motocycleDtos = EntityListToDto(result);

            var apiReponse = new ApiResponse()
            {
                Data = motocycleDtos?.Count != 0 ? motocycleDtos : null,
                Message = result?.Count == 0 ? "Dados inválidos" : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> DeleteById(string? id)
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

            return apiReponse!;
        }

        #endregion

        #region Internal

        public async Task<string?> ValidateDelete(string? id)
        {
            var message = new StringBuilder();

            if (string.IsNullOrEmpty(id))
            {
                message.Append($"Invalid: {nameof(id)} plate: {id} does not exist; ");
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(id);
                if (motorcycle == null)
                {
                    message.Append($"Invalid: {nameof(id)} plate: {id} does not exist; ");
                }

                var motorcycleIsInUse = await _rentalRepository.CheckMotorcycleIsAvaliableAsync(id);
                if (motorcycleIsInUse)
                {
                    message.Append($"Invalid: {nameof(id)} has rental is in use; ");
                }
            }

            return message.ToString();
        }

        public async Task<string?> ValidatorCreateAsync(MotorcycleDto data)
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

                    var isUnicId = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(normalizedPlate);

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

        public async Task<string> GetModelId(string modelNormalized)
        {
            var result = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

            return result;
        }

        public async Task SendNotification(MotorcycleEntity motocycle)
        {
            if (motocycle.YearManufacture == NOTIFICATION_YEAR_MANUFACTORY)
            {
                //TODO: HEAVY MISSION NOTIFICATION
            }
        }
        #endregion
    }
}
