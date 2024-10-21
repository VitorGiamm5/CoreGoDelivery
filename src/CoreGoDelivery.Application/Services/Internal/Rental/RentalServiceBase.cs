using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalServiceBase
    {
        public readonly IRentalRepository _repositoryRental;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IMotocycleRepository _repositoryMotocyle;
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly IBaseInternalServices _baseInternalServices;

        public const string CURRENCY_BRL = "R$";

        public const double PENALTY_VALUE_PER_DAY = 50;
        public const double MINIMAL_DAYS_PLAN = 7;
        public const double MINIMAL_FEE_PERCENTAGE = 20;
        public const double DEFAULT_FEE_PERCENTAGE = 40;

        public RentalServiceBase(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier,
            IBaseInternalServices baseInternalServices)
        {
            _repositoryRental = repositoryRental;
            _repositoryPlan = repositoryPlan;
            _repositoryMotocyle = repositoryMotocyle;
            _repositoryDeliverier = repositoryDeliverier;
            _baseInternalServices = baseInternalServices;
        }

        #region Mappers

        public static RentalEntity MapCreateToEntity(RentalCreateCommand data, RentalEntity RentalDates)
        {
            var result = new RentalEntity()
            {
                Id = Ulid.NewUlid().ToString(),
                StartDate = RentalDates.StartDate,
                EndDate = RentalDates.EndDate,
                EstimatedReturnDate = RentalDates.EstimatedReturnDate,
                ReturnedToBaseDate = null,
                DeliverierId = data.DeliverierId,
                MotorcycleId = data.MotorcycleId,
                RentalPlanId = data.PlanId
            };

            return result;
        }

        #endregion

        /// <summary>
        /// used to Update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public string? BuildCalculatePenalty(DateTime returnedToBaseDate, RentalEntity? rental)
        {
            TimeSpan buildDiffDays = returnedToBaseDate - rental!.EstimatedReturnDate;

            int diffDays = buildDiffDays.Days;

            if (diffDays == 0)
            {
                return "Data de devolução informada com sucesso";
            }

            var message = new StringBuilder();

            message.AppendLine($"Value to pay: {CURRENCY_BRL} ");


            if (diffDays < 0)
            {
                diffDays = diffDays * -1;

                var isPlanminimalPlan = rental?.RentalPlan?.DaysQuantity == MINIMAL_DAYS_PLAN;

                double feePercentPenalty = isPlanminimalPlan
                    ? MINIMAL_FEE_PERCENTAGE / 100
                    : DEFAULT_FEE_PERCENTAGE / 100;

                var valueDaysRemain = rental!.RentalPlan!.DayliCost * diffDays;

                var penaltyValue = valueDaysRemain * feePercentPenalty;

                var penaltyValueRounded = Math.Round(penaltyValue, 2);

                message.Append($"{penaltyValueRounded}");
            }
            else
            {
                var penaltyValue = diffDays * PENALTY_VALUE_PER_DAY;

                message.Append($"{penaltyValue}");
            }

            var result = _baseInternalServices.BuildMessageValidator(message);

            return result;
        }

        /// <summary>
        /// used to Update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public string? ValidadeToPlan(RentalCreateCommand data, RentalPlanEntity plan)
        {
            var message = new StringBuilder();

            var refence = CalculateDatesByPlan(plan);

            #region StartDate validate
            if (!string.IsNullOrEmpty(data.StartDate))
            {
                data.StartDate = refence.StartDate.ToString();

                if (DateTime.Parse(data.StartDate) != refence.StartDate)
                {
                    message.AppendErrorWithExpexted(message, data.StartDate, refence.StartDate.ToString());
                }
            }
            #endregion

            #region EndDate validate
            if (!string.IsNullOrEmpty(data.EndDate))
            {
                data.EndDate = refence.EndDate.ToString();

                if (DateTime.Parse(data.EndDate) != refence.EndDate)
                {
                    message.AppendErrorWithExpexted(message, data.EndDate, refence.EndDate.ToString());
                }
            }
            #endregion

            #region EstimatedReturnDate validate
            if (!string.IsNullOrEmpty(data.EstimatedReturnDate))
            {
                data.StartDate = refence.EstimatedReturnDate.ToString();

                if (DateTime.Parse(data.EstimatedReturnDate) != refence.EstimatedReturnDate)
                {
                    message.AppendErrorWithExpexted(message, data.EstimatedReturnDate, refence.EstimatedReturnDate.ToString());
                }
            }
            #endregion

            #region DayliCost validate
            if (data.DayliCost == null)
            {
                data.DayliCost = plan.DayliCost;

                if (data.DayliCost != plan.DayliCost)
                {
                    message.AppendErrorWithExpexted(message, data.DayliCost, plan.DayliCost.ToString());
                }
            }
            #endregion

            return _baseInternalServices.BuildMessageValidator(message);
        }

        /// <summary>
        /// used to: CalculateDatesByPlan
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static RentalEntity CalculateDatesByPlan(RentalPlanEntity plan)
        {
            var result = new RentalEntity()
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(plan.DaysQuantity),
                EstimatedReturnDate = DateTime.UtcNow.AddDays(plan.DaysQuantity)
            };

            return result;
        }

        #region Update Validators

        /// <summary>
        /// used to Update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public async Task<string?> BuilderUpdateValidator(RentalReturnedToBaseDateCommand? data)
        {
            var message = new StringBuilder();
            var id = data!.Id;

            #region Id validator

            var isValidIdParam = _baseInternalServices.RequestIdParamValidator(id);

            if (isValidIdParam)
            {
                message.AppendError(message, id);
                return message.ToString();
            }

            var rentalEntity = await _repositoryRental.GetByIdAsync(id);

            if (rentalEntity == null)
            {
                message.AppendError(message, id, AdditionalMessageEnum.NotFound);
            }
            else
            {
                var motorcycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(rentalEntity.MotorcycleId);
                if (!motorcycleIsAvaliable)
                {
                    message.AppendLine($"Invalid field: {nameof(rentalEntity.ReturnedToBaseDate)} was returned previously at {rentalEntity.ReturnedToBaseDate}; ");
                }
            }

            #endregion

            #region Returned To Base Date validator

            if (data?.ReturnedToBaseDate == null)
            {
                message.AppendError(message, data?.ReturnedToBaseDate);
            }
            else
            {
                var isAfterDateStart = data?.ReturnedToBaseDate >= rentalEntity?.StartDate;
                if (!isAfterDateStart)
                {
                    message.Append($"Invalid field: {nameof(data.ReturnedToBaseDate)} : {data?.ReturnedToBaseDate} must be after {nameof(rentalEntity.StartDate)} : {rentalEntity?.StartDate}; ");
                }
            }

            #endregion

            return _baseInternalServices.BuildMessageValidator(message);

        }

        #endregion

        #region Create validator

        /// <summary>
        /// used to: Create Rental
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public async Task<string?> BuilderCreateValidator(RentalCreateCommand data)
        {
            var message = new StringBuilder();

            #region PlanId validator
            if (string.IsNullOrWhiteSpace(data.PlanId.ToString()))
            {
                message.AppendError(message, data.PlanId);
            }
            else
            {
                var plan = await _repositoryPlan.GetById(data.PlanId);
                if (plan == null)
                {
                    message.AppendError(message, data.PlanId, AdditionalMessageEnum.NotFound);
                }
                else
                {
                    var resultValidateDates = ValidadeToPlan(data, plan);
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
                message.AppendError(message, data.MotorcycleId);
            }
            else
            {
                var existMotocycleId = await _repositoryMotocyle.GetOneByIdAsync(data.MotorcycleId);
                if (existMotocycleId == null)
                {
                    message.AppendError(message, data.MotorcycleId, AdditionalMessageEnum.NotFound);
                }

                var motocycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(data.MotorcycleId);
                if (!motocycleIsAvaliable)
                {
                    message.AppendError(message, data.MotorcycleId, AdditionalMessageEnum.Unavailable);
                }
            }
            #endregion

            #region DeliverierId validator
            if (string.IsNullOrWhiteSpace(data.DeliverierId))
            {
                message.AppendError(message, data.DeliverierId);
            }
            else
            {
                var existDeliverierId = await _repositoryDeliverier.CheckIsUnicById(data.DeliverierId);
                if (!existDeliverierId)
                {
                    message.AppendError(message, data.DeliverierId, AdditionalMessageEnum.NotFound);
                }
            }
            #endregion

            return _baseInternalServices.BuildMessageValidator(message);
        }

        #endregion
    }
}