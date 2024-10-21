using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle
{
    public class MotorcycleServiceBase
    {
        public readonly IMotocycleRepository _repositoryMotorcycle;
        public readonly IModelMotocycleRepository _repositoryModelMotorcycle;
        public readonly IRentalRepository _rentalRepository;
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly RabbitMQPublisher _publisher;

        public const int YEAR_MANUFACTORY_TO_SEND_MESSAGE = 2024;

        public MotorcycleServiceBase(
            IMotocycleRepository repositoryMotorcycle, 
            IModelMotocycleRepository repositoryModelMotorcycle, 
            IRentalRepository rentalRepository, 
            IBaseInternalServices baseInternalServices, 
            RabbitMQPublisher publisher)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _repositoryModelMotorcycle = repositoryModelMotorcycle;
            _rentalRepository = rentalRepository;
            _baseInternalServices = baseInternalServices;
            _publisher = publisher;
        }

        #region Mappers
        public MotorcycleEntity MapCreateToEntity(MotorcycleCreateCommand data)
        {
            var result = new MotorcycleEntity()
            {
                Id = _baseInternalServices.IdBuild(data.Id),
                YearManufacture = data.YearManufacture,
                ModelMotorcycleId = data.ModelName,
                PlateNormalized = _baseInternalServices.RemoveCharacteres(data.PlateId)
            };

            return result;
        }

        public static List<MotorcycleCreateCommand> MapEntityListToDto(List<MotorcycleEntity>? entity)
        {
            List<MotorcycleCreateCommand> motocycleDtos = [];

            if (entity != null)
            {
                var resultDto = motocycleDtos = entity
                     .Select(motorcycle => MapEntityToDto(motorcycle))
                     .ToList();

                return resultDto;
            }

            return new List<MotorcycleCreateCommand>();
        }

        public static MotorcycleCreateCommand MapEntityToDto(MotorcycleEntity motorcycle)
        {
            var restult = new MotorcycleCreateCommand
            {
                Id = motorcycle.Id,
                YearManufacture = motorcycle.YearManufacture,
                ModelName = motorcycle!.ModelMotorcycle!.Name,
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

        public async Task<string?> BuilderDeleteValidator(string? idMotorcycle)
        {
            var message = new StringBuilder();

            if (string.IsNullOrEmpty(idMotorcycle))
            {
                message.AppendError(message, nameof(idMotorcycle));
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle);
                if (motorcycle == null)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
                }

                var motorcycleIsInUse = await _rentalRepository.CheckMotorcycleIsAvaliableAsync(idMotorcycle);
                if (motorcycleIsInUse)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.Unavailable);
                }
            }

            return _baseInternalServices.BuildMessageValidator(message);
        }

        #endregion

        #region Builder Create validator

        public async Task<string?> BuilderCreateValidator(MotorcycleCreateCommand data)
        {
            var message = new StringBuilder();

            BuildMessageYear(data, message);

            await BuildMessageIdMotorcycle(data, message);
            await BuildMessagePlate(data.PlateId, message);
            await BuildMessageModelMotorcycle(data, message);

            return _baseInternalServices.BuildMessageValidator(message);
        }

        public static void BuildMessageYear(MotorcycleCreateCommand data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.YearManufacture.ToString()))
            {
                message.AppendError(message, nameof(data.YearManufacture));
            }
            else
            {
                if (data.YearManufacture <= 1903)
                {
                    message.AppendError(message, nameof(data.YearManufacture), AdditionalMessageEnum.Unavailable);
                }
            }
        }

        public async Task BuildMessagePlate(string? plate, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(plate))
            {
                message.AppendError(message, nameof(plate));
            }
            else
            {
                var isValidPlate = PlateValidator(plate);

                if (isValidPlate)
                {
                    var normalizedPlate = _baseInternalServices.RemoveCharacteres(plate);

                    var isUnicId = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(normalizedPlate);

                    if (!isUnicId)
                    {
                        message.AppendError(message, nameof(plate), AdditionalMessageEnum.AlreadyExist);
                    }
                }
                else
                {
                    message.AppendError(message, nameof(plate), AdditionalMessageEnum.InvalidFormat);
                }
            }
        }

        public async Task BuildMessageIdMotorcycle(MotorcycleCreateCommand data, StringBuilder message)
        {
            var idMotorcycle = data.Id;
            if (!string.IsNullOrWhiteSpace(idMotorcycle))
            {
                var isUnicId = await _repositoryMotorcycle.CheckIsUnicById(idMotorcycle);

                if (!isUnicId)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.AlreadyExist);
                }
            }
        }

        public async Task BuildMessageModelMotorcycle(MotorcycleCreateCommand data, StringBuilder message)
        {
            var idMotorcycle = data.Id;

            if (string.IsNullOrWhiteSpace(data.ModelName))
            {
                message.AppendError(message, nameof(data.ModelName));
            }
            else
            {
                var modelNormalized = _baseInternalServices.RemoveCharacteres(data.ModelName);

                var modelId = await _repositoryModelMotorcycle.GetIdByModelName(modelNormalized);

                if (string.IsNullOrEmpty(modelId))
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
                }

                data.ModelName = modelId;
            }
        }

        #endregion

        #region Builder Update validator

        public async Task<string?> ChangePlateValidator(string? id, string? plate)
        {
            var message = new StringBuilder();


            await BuildMessagePlate(id, message);

            await MessageBuildChangePlate(plate, message);

            return _baseInternalServices.BuildMessageValidator(message);
        }

        public async Task BuildMessageChangePlateId(string? idMotorcycle, StringBuilder message)
        {
            var isValidId = _baseInternalServices.RequestIdParamValidator(idMotorcycle);

            if (!isValidId)
            {
                message.AppendError(message, nameof(idMotorcycle));
            }
            else
            {
                var motorcycle = await _repositoryMotorcycle.GetOneByIdAsync(idMotorcycle!);

                if (motorcycle == null)
                {
                    message.AppendError(message, nameof(idMotorcycle), AdditionalMessageEnum.NotFound);
                }
            }
        }

        public async Task MessageBuildChangePlate(string? plate, StringBuilder message)
        {
            if (string.IsNullOrEmpty(plate))
            {
                message.AppendError(message, nameof(plate));
            }
            else
            {
                var isValidPlate = PlateValidator(plate!);
                if (!isValidPlate)
                {
                    message.AppendError(message, nameof(plate), AdditionalMessageEnum.InvalidFormat);
                }
                else
                {
                    var plateIsUnic = await _repositoryMotorcycle.CheckIsUnicByPlateAsync(plate);
                    if (!plateIsUnic)
                    {
                        message.AppendError(message, nameof(plate), AdditionalMessageEnum.MustBeUnic);
                    }
                }
            }
        }

        #endregion

        #region External Service

        public async Task SendNotification(MotorcycleEntity motocycle)
        {
            if (motocycle.YearManufacture == YEAR_MANUFACTORY_TO_SEND_MESSAGE)
            {
                var motorcycleNotification = new NotificationMotorcycleDto()
                {
                    Id = Ulid.NewUlid().ToString(),
                    IdMotorcycle = motocycle.Id,
                    YearManufacture = motocycle.YearManufacture,
                    CreatedAt = DateTime.UtcNow
                };

                _publisher.PublishMotorcycle(motorcycleNotification);

                Console.WriteLine("Motocicleta publicada na fila.");
            }
        }

        #endregion
    }
}