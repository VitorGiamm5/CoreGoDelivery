using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalServiceBase
    {
        public const string MESSAGE_INVALID_DATA = "Dados inválidos";
        public const string CURRENCY_BRL = "R$";

        public const double PENALTY_VALUE_PER_DAY = 50;
        public const double MINIMAL_DAYS_PLAN = 7;
        public const double MINIMAL_FEE_PERCENTAGE = 20;
        public const double DEFAULT_FEE_PERCENTAGE = 40;

        public static string CalculatePenalty(DateTime returnedToBaseDate, RentalEntity? rental)
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

            return message.ToString();
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

        public static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
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

            return message.ToString();
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
    }
}