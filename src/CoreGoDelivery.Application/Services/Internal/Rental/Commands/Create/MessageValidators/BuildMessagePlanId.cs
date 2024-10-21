using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators
{
    public class BuildMessagePlanId
    {
        public readonly IRentalPlanRepository _repositoryPlan;

        public readonly PlanMotorcycleValidator _planValidator;

        public BuildMessagePlanId(
            IRentalPlanRepository repositoryPlan, 
            PlanMotorcycleValidator planValidator)
        {
            _repositoryPlan = repositoryPlan;
            _planValidator = planValidator;
        }

        public async Task Build(RentalCreateCommand data, StringBuilder message, string paramName)
        {
            if (string.IsNullOrWhiteSpace(data.PlanId.ToString()))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                var plan = await _repositoryPlan.GetById(data.PlanId);

                if (plan == null)
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.NotFound);
                }
                else
                {
                    var resultValidateDates = _planValidator.Validade(data, plan);

                    if (!string.IsNullOrWhiteSpace(resultValidateDates))
                    {
                        message.Append(resultValidateDates);
                    }
                }
            }
        }
    }
}
