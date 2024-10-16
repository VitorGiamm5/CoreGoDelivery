using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motocycle
{
    public class MotocycleService : IMotocycleService
    {
        private readonly IMotocycleRepository _repositoryMotocycle;
        private readonly IModelMotocycleRepository _modelMotocycleRepository;

        private const int NOTIFICATION_YEAR_MANUFACTORY = 2024;
        private const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public MotocycleService(IMotocycleRepository repositoryMotocycle, IModelMotocycleRepository modelMotocycleRepository)
        {
            _repositoryMotocycle = repositoryMotocycle;
            _modelMotocycleRepository = modelMotocycleRepository;
        }

        #region Public
        public async Task<ApiResponse> ChangePlate(string id, PlateIdDto data)
        {
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
                Message = await ValidatorMotocycleAsync(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var motocycle = new MotocycleEntity()
            {
                Id = SelectId(data),
                YearManufacture = data.YearManufacture,
                ModelMotocycleId = data.ModelName,
                PlateIdNormalized = RemoveCharacteres(data.PlateId)
            };

            var resultCreate = await _repositoryMotocycle.Create(motocycle);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            await SendNotification(motocycle);

            return apiReponse;
        }

        public async Task<ApiResponse> Delete(PlateIdDto id)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> GetOne(string data)
        {
            var result = new MotocycleDto()
            {
                Id = "moto123",
                YearManufacture = 2020,
                ModelName = "Mottu Sport",
                PlateId = "CDX-0101"
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> List(PlateIdDto? data)
        {
            List<MotocycleDto> result = [
                new MotocycleDto()
                {
                    Id = "moto123",
                    YearManufacture = 2020,
                    ModelName = "Mottu Sport",
                    PlateId = "CDX-0101"
                }
            ];

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return apiReponse;
        }

        #endregion

        #region Private

        private async Task SendNotification(MotocycleEntity motocycle)
        {
            if (motocycle.YearManufacture == NOTIFICATION_YEAR_MANUFACTORY)
            {
                //TODO: HEAVY MISSION NOTIFICATION
            }
        }

        private static string SelectId(MotocycleDto data)
        {
            var result = string.IsNullOrEmpty(data.Id) ? Ulid.NewUlid().ToString() : data.Id;

            return result;
        }

        private async Task<string> GetModelMotocycle(string modelName)
        {
            throw new NotImplementedException();
        }

        private static string RemoveCharacteres(string plateId)
        {
            var result = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            return result;
        }

        private string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }

        private async Task<string?> ValidatorMotocycleAsync(MotocycleDto data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryMotocycle.CheckIsUnicById(data.Id);

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
                    var isUnicId = await _repositoryMotocycle.CheckIsUnicByPlateId(normalizedPlate);

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
            if (message.Length > 0) return message.ToString();
            #endregion

            return null;
        }

        private static bool ValidatePlate(string plateId)
        {
            var plate = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            if (Regex.IsMatch(plate, @"^[A-Z]{3}\d{4}$") ||    // old format
                Regex.IsMatch(plate, @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$")) // new format
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<string> GetModelId(string modelNormalized)
        {
            var result = await _modelMotocycleRepository.GetIdByModelName(modelNormalized);

            return result;
        }

        #endregion
    }
}
