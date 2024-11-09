using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;

public class RentalBuildMessagePlanId
{
    public readonly IRentalPlanRepository _repositoryPlan;

    public RentalBuildMessagePlanId(
        IRentalPlanRepository repositoryPlan)
    {
        _repositoryPlan = repositoryPlan;
    }

    public async Task Build(RentalCreateCommand data, StringBuilder message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(data.PlanId.ToString()))
        {
            message.Append(paramName);
        }
        else
        {
            var plan = await _repositoryPlan.GetById(data.PlanId);

            if (plan == null)
            {
                message.Append(paramName.AppendError(AdditionalMessageEnum.NotFound));
            }
            else
            {
                var resultValidateDates = RentalPlanMotorcycleValidator.Validade(data, plan);

                if (resultValidateDates != null)
                {
                    message.Append(resultValidateDates);
                }
            }
        }
    }
}
