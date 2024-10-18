using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalServiceBase : BaseInternalServices
    {
        public readonly IRentalRepository _repositoryRental;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IMotocycleRepository _repositoryMotocyle;
        public readonly IDeliverierRepository _repositoryDeliverier;

        public const string CURRENCY_BRL = "R$";

        public const double PENALTY_VALUE_PER_DAY = 50;
        public const double MINIMAL_DAYS_PLAN = 7;
        public const double MINIMAL_FEE_PERCENTAGE = 20;
        public const double DEFAULT_FEE_PERCENTAGE = 40;

        public RentalServiceBase(
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

        public static string? CalculatePenalty(DateTime returnedToBaseDate, RentalEntity? rental)
        {
            TimeSpan buildDiffDays = returnedToBaseDate - rental!.EstimatedReturnDate;

            int diffDays = buildDiffDays.Days;

            if (diffDays == 0)
            {
                return "Data de devolução informada com sucesso";
            }

            var message = new StringBuilder();

            message.Append($"Value to pay: {CURRENCY_BRL} ");


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

            return BuildMessageValidator(message);
        }

        public static RentalEntity CreateToEntity(RentalDto data, RentalEntity RentalDates)
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

        public static string? ValidadeToPlan(ref RentalDto data, RentalPlanEntity plan)
        {
            var message = new StringBuilder();

            var refence = CalculateDatesByPlan(plan);

            #region StartDate validate
            if (!string.IsNullOrEmpty(data.StartDate))
            {
                data.StartDate = refence.StartDate.ToString();

                if (DateTime.Parse(data.StartDate) != refence.StartDate)
                {
                    message.Append($"Invalid: {nameof(data.StartDate)}: {DateTime.Parse(data.StartDate)}, expected: {refence.StartDate}; ");
                }
            }
            #endregion

            #region EndDate validate
            if (!string.IsNullOrEmpty(data.EndDate))
            {
                data.EndDate = refence.EndDate.ToString();

                if (DateTime.Parse(data.EndDate) != refence.EndDate)
                {
                    message.Append($"Invalid: {nameof(data.EndDate)}: {DateTime.Parse(data.EndDate)}, expected: {refence.EndDate}; ");
                }
            }
            #endregion

            #region EstimatedReturnDate validate
            if (!string.IsNullOrEmpty(data.EstimatedReturnDate))
            {
                data.StartDate = refence.EstimatedReturnDate.ToString();

                if (DateTime.Parse(data.EstimatedReturnDate) != refence.EstimatedReturnDate)
                {
                    message.Append($"Invalid: {nameof(data.EstimatedReturnDate)}: {DateTime.Parse(data.EstimatedReturnDate)}, expected: {refence.EstimatedReturnDate}; ");
                }
            }
            #endregion

            #region DayliCost validate
            if (data.DayliCost == null)
            {
                data.DayliCost = plan.DayliCost;

                if (data.DayliCost != plan.DayliCost)
                {
                    message.Append($"Invalid: {nameof(data.DayliCost)}: {data.DayliCost}, expected: {plan.DayliCost}; ");
                }
            }
            #endregion

            return BuildMessageValidator(message);
        }

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

        #region Validators

        public async Task<string?> BuilderUpdateValidator(string id, ReturnedToBaseDateDto? data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (RequestIdParamValidator(id))
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