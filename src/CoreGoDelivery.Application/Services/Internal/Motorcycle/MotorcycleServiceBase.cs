using CoreGoDelivery.Domain.DTO.Motorcycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle
{
    public class MotorcycleServiceBase : BaseInternalServices
    {
        public readonly IMotocycleRepository _repositoryMotorcycle;
        public readonly IModelMotocycleRepository _repositoryModelMotorcycle;
        public readonly IRentalRepository _rentalRepository;

        public const int YEAR_MANUFACTORY_TO_SEND_MESSAGE = 2024;

        public MotorcycleServiceBase(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _repositoryModelMotorcycle = repositoryModelMotorcycle;
            _rentalRepository = rentalRepository;
        }

        #region Mappers
        public static MotorcycleEntity MapCreateToEntity(MotorcycleDto data)
        {
            var result = new MotorcycleEntity()
            {
                Id = IdBuild(data.Id),
                YearManufacture = data.YearManufacture,
                ModelMotorcycleId = data.ModelName,
                PlateNormalized = RemoveCharacteres(data.PlateId)
            };

            return result;
        }

        public static List<MotorcycleDto> MapEntityListToDto(List<MotorcycleEntity>? entity)
        {
            List<MotorcycleDto> motocycleDtos = [];

            if (entity != null)
            {
                var resultDto = motocycleDtos = entity
                     .Select(motorcycle => MapEntityToDto(motorcycle))
                     .ToList();

                return resultDto;
            }

            return new List<MotorcycleDto>();
        }

        public static MotorcycleDto MapEntityToDto(MotorcycleEntity motorcycle)
        {
            var restult = new MotorcycleDto
            {
                Id = motorcycle.Id,
                YearManufacture = motorcycle.YearManufacture,
                ModelName = motorcycle.ModelMotorcycle.Name,
                PlateId = motorcycle.PlateNormalized
            };

            return restult;
        }

        #endregion

        #region Validators

        public static bool PlateValidator(string? plateId)
        {
            if (plateId == null)
            {
                return false;
            }

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

        public async Task<string?> BuilderDeleteValidator(string? id)
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
                    message.Append($"Invalid: {nameof(id)} has a rental is in use; ");
                }
            }

            return BuildMessageValidator(message);
        }

        #endregion

        #region Builder Create validator

        public async Task<string?> BuilderCreateValidator(MotorcycleDto data)
        {
            var message = new StringBuilder();

            BuildMessageYear(data, message);

            await Task.WhenAll(
                BuildMessageIdMotorcycle(data, message),
                BuildMessagePlate(data, message),
                BuildMessageModelMotorcycle(data, message)
            );

            return BuildMessageValidator(message);
        }

        public static void BuildMessageYear(MotorcycleDto data, StringBuilder message)
        {
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
        }

        public async Task BuildMessagePlate(MotorcycleDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.PlateId))
            {
                message.Append($"Empty: {nameof(data.PlateId)}; ");
            }
            else
            {
                var isValidPlate = PlateValidator(data.PlateId);
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
        }

        public async Task BuildMessageIdMotorcycle(MotorcycleDto data, StringBuilder message)
        {
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryMotorcycle.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.Append($"{nameof(data.Id)}: {data.Id} already exists; ");
                }
            }
        }

        public async Task BuildMessageModelMotorcycle(MotorcycleDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.ModelName))
            {
                message.Append($"Empty: {nameof(data.ModelName)}; ");
            }
            else
            {
                var modelNormalized = RemoveCharacteres(data.ModelName);
                var modelId = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

                if (string.IsNullOrEmpty(modelId))
                {
                    message.Append($"Invalid: {nameof(data.ModelName)}: {data.ModelName} not exist; ");
                }

                data.ModelName = modelId;
            }
        }

        #endregion

        #region Builder Update validator

        public async Task<string?> ChangePlateValidator(string? id, string? plate)
        {
            var message = new StringBuilder();

            await BuildMessageChangePlateId(id, message);

            await MessageBuildChangePlate(plate, message);

            return BuildMessageValidator(message);
        }

        public async Task BuildMessageChangePlateId(string? id, StringBuilder message)
        {
            var isValidId = RequestIdParamValidator(id);

            if (!isValidId)
            {
                message.Append($"Invalid: {nameof(id)}, type: {nameof(id.GetType)}, value: {id}; ");
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(id);
                if (motorcycle == null)
                {
                    message.Append($"Invalid: {nameof(id)} : {id} not exists; ");
                }
            }
        }

        public async Task MessageBuildChangePlate(string? plate, StringBuilder message)
        {
            if (string.IsNullOrEmpty(plate))
            {
                message.Append($"Empty: {nameof(plate)}; ");
            }
            else
            {
                var isValidPlate = PlateValidator(plate!);
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
        }

        //RequestIdParamValidator




        #endregion

        #region External Service

        public async Task SendNotification(MotorcycleEntity motocycle)
        {
            if (motocycle.YearManufacture == YEAR_MANUFACTORY_TO_SEND_MESSAGE)
            {
                //TODO: HEAVY MISSION NOTIFICATION
            }
        }

        #endregion
    }
}