using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalService : RentalServiceBase, IRentalService
    {
        public readonly IRentalRepository _repositoryRental;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IMotocycleRepository _repositoryMotocyle;
        public readonly IDeliverierRepository _repositoryDeliverier;

        public RentalService(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier)
        {
            _repositoryRental = repositoryRental;
            _repositoryPlan = repositoryPlan;
            _repositoryMotocyle = repositoryMotocyle;
            _repositoryDeliverier = repositoryDeliverier;
        }

        #region To Controller

        public async Task<ApiResponse> Create(RentalDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await CreateValidator(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plan = await _repositoryPlan.GetById(data.PlanId);

            var calculatedDates = CalculateDatesByPlan(plan!);

            var rental = CreateToEntity(data, calculatedDates);

            var resultCreate = await _repositoryRental.Create(rental);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        public async Task<ApiResponse> GetById(string? id)
        {
            var message = new StringBuilder();
            RentalEntity? rental = null;

            if (string.IsNullOrEmpty(id) || id == ":id")
            {
                message.Append($"Invalid: {nameof(id)} is empty; ");
            }
            else
            {
                rental = await _repositoryRental.GetByIdAsync(id);

                if (rental == null)
                {
                    message.Append($"Invalid: {nameof(id)} no data found; ");
                }
            }

            var rentalDto = new
            {
                identificador = rental!.Id,
                valor_diaria = rental.RentalPlan!.DayliCost,
                entregador_id = rental.DeliverierId,
                moto_id = rental.MotorcycleId,
                data_inicio = rental.StartDate,
                data_termino = rental.EndDate,
                data_previsao_termino = rental.EstimatedReturnDate,
                data_devolucao = rental.ReturnedToBaseDate,
            };

            var apiReponse = new ApiResponse()
            {
                Data = rentalDto,
                Message = message != null ? message.ToString() : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> UpdateReturnedToBaseDate(string id, ReturnedToBaseDateDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await UpdateValidator(id, data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var rental = await _repositoryRental.GetByIdAsync(id);

            var returnedDate = data!.ReturnedToBaseDate!.Value;

            string result = CalculatePenalty(returnedDate, rental) ?? MESSAGE_INVALID_DATA;

            var successUpdate = await _repositoryRental.UpdateReturnedToBaseDate(id, returnedDate);

            if (!successUpdate)
            {
                apiReponse.Message = $"Error: fail to update {nameof(data.ReturnedToBaseDate)}; ";
                return apiReponse;
            }

            apiReponse.Data = new { messagem = result };

            return apiReponse;
        }

        #endregion

        #region Validators

        public async Task<string?> UpdateValidator(string id, ReturnedToBaseDateDto? data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (string.IsNullOrEmpty(id) || id == ":id")
            {
                message.Append($"Invalid: {nameof(id)} is empty; ");
                return message.ToString();
            }

            var rentalEntity = await _repositoryRental.GetByIdAsync(id);

            if (rentalEntity == null)
            {
                message.Append($"Invalid: {nameof(id)} no data found; ");
            }
            else
            {
                var motorcycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(rentalEntity.MotorcycleId);
                if (!motorcycleIsAvaliable)
                {
                    message.Append($"Invalid: {nameof(rentalEntity.ReturnedToBaseDate)} was returned previously at {rentalEntity.ReturnedToBaseDate}; ");
                }
            }

            #endregion

            #region Returned To Base Date validator

            if (data?.ReturnedToBaseDate == null)
            {
                message.Append($"Invalid: {nameof(data.ReturnedToBaseDate)} is empty; ");
            }
            else
            {
                var isAfterDateStart = data?.ReturnedToBaseDate >= rentalEntity?.StartDate;
                if (!isAfterDateStart)
                {
                    message.Append($"Invalid: {nameof(data.ReturnedToBaseDate)} : {data?.ReturnedToBaseDate} must be after {nameof(rentalEntity.StartDate)} : {rentalEntity?.StartDate}; ");
                }
            }

            #endregion

            return BuildMessageValidator(message);

        }

        public async Task<bool> MotorcycleIsAvaliableValidator(string id)
        {
            var motocycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(id);

            return motocycleIsAvaliable;
        }

        public async Task<string?> CreateValidator(RentalDto data)
        {
            var message = new StringBuilder();

            #region PlanId validator
            if (string.IsNullOrWhiteSpace(data.PlanId.ToString()))
            {
                message.Append($"Empty: {nameof(data.PlanId)}; ");
            }
            else
            {
                var plan = await _repositoryPlan.GetById(data.PlanId);
                if (plan == null)
                {
                    message.Append($"Invalid: {nameof(data.PlanId)}: {data.PlanId} not exist; ");
                }
                else
                {
                    var resultValidateDates = ValidadeToPlan(ref data, plan);
                    if (!string.IsNullOrWhiteSpace(resultValidateDates))
                    {
                        message.Append(resultValidateDates);
                    }
                }
            }
            #endregion

            #region MotorcycleId validator
            if (string.IsNullOrWhiteSpace(data.MotorcycleId))
            {
                message.Append($"Empty: {nameof(data.MotorcycleId)}; ");
            }
            else
            {
                var existMotocycleId = await _repositoryMotocyle.GetOneByIdAsync(data.MotorcycleId);
                if (existMotocycleId == null)
                {
                    message.Append($"Invalid: {nameof(data.MotorcycleId)}: {data.MotorcycleId} not exist; ");
                }

                var motocycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(data.MotorcycleId);
                if (!motocycleIsAvaliable)
                {
                    message.Append($"Invalid: {nameof(data.MotorcycleId)}: {data.MotorcycleId} is not avaliable; ");
                }
            }
            #endregion

            #region DeliverierId validator
            if (string.IsNullOrWhiteSpace(data.DeliverierId))
            {
                message.Append($"Empty:  {nameof(data.DeliverierId)}; ");
            }
            else
            {
                var existDeliverierId = await _repositoryDeliverier.CheckIsUnicById(data.DeliverierId);
                if (!existDeliverierId)
                {
                    message.Append($"Invalid: {nameof(data.DeliverierId)}: {data.DeliverierId} not exist; ");
                }
            }
            #endregion

            return BuildMessageValidator(message);
        }

        #endregion
    }
}
