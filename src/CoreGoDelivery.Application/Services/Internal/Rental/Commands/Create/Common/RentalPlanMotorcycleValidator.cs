using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;

public static class RentalPlanMotorcycleValidator
{
    public static StringBuilder? Validade(RentalCreateCommand data, RentalPlanEntity plan)
    {
        var message = new StringBuilder();

        var refence = RentalCalculateDatesByPlan.Calculate(plan);

        #region StartDate validate

        if (!string.IsNullOrEmpty(data.StartDate))
        {
            data.StartDate = refence.StartDate.ToString();

            if (DateTime.Parse(data.StartDate) != refence.StartDate)
            {
                message.Append(data.StartDate.ToString().AppendError(AdditionalMessageEnum.InvalidDate));
            }
        }

        #endregion

        #region EndDate validate

        if (!string.IsNullOrEmpty(data.EndDate))
        {
            data.EndDate = refence.EndDate.ToString();

            if (DateTime.Parse(data.EndDate) != refence.EndDate)
            {
                message.Append(data.EndDate.ToString().AppendError(AdditionalMessageEnum.InvalidDate));
            }
        }

        #endregion

        #region EstimatedReturnDate validate

        if (!string.IsNullOrEmpty(data.EstimatedReturnDate))
        {
            data.StartDate = refence.EstimatedReturnDate.ToString();

            if (DateTime.Parse(data.EstimatedReturnDate) != refence.EstimatedReturnDate)
            {
                message.Append(data.EstimatedReturnDate.ToString().AppendError(AdditionalMessageEnum.InvalidDate));
            }
        }

        #endregion

        return message;
    }
}
