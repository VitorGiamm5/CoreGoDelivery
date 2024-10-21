using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common
{
    public class PlanMotorcycleValidator
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly CalculateDatesByPlan _calculateDatesByPlan;

        public PlanMotorcycleValidator(
            IBaseInternalServices baseInternalServices,
            CalculateDatesByPlan calculateDatesByPlan)
        {
            _baseInternalServices = baseInternalServices;
            _calculateDatesByPlan = calculateDatesByPlan;
        }

        public string? Validade(RentalCreateCommand data, RentalPlanEntity plan)
        {
            var message = new StringBuilder();

            var refence = _calculateDatesByPlan.Calculate(plan);

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
    }
}
