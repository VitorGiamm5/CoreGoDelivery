using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public class RentalCreateValidate
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IRentalRepository _repositoryRental;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly IMotocycleRepository _repositoryMotorcycle;

        public readonly PlanMotorcycleValidator _planValidator;

        public async Task<string?> BuilderCreateValidator(RentalCreateCommand data)
        {
            var message = new StringBuilder();

            var paramName = nameof(data.PlanId);

            #region PlanId validator
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
            #endregion

            #region MotorcycleId validator
            if (string.IsNullOrWhiteSpace(data.MotorcycleId))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                var existMotocycleId = await _repositoryMotorcycle.GetOneByIdAsync(data.MotorcycleId);
                if (existMotocycleId == null)
                {
                    message.AppendError(message, nameof(data.MotorcycleId), AdditionalMessageEnum.NotFound);
                }

                var motocycleIsAvaliable = await _repositoryRental.CheckMotorcycleIsAvaliableAsync(data.MotorcycleId);
                if (!motocycleIsAvaliable)
                {
                    message.AppendError(message, nameof(data.MotorcycleId), AdditionalMessageEnum.Unavailable);
                }
            }
            #endregion

            #region DeliverierId validator
            if (string.IsNullOrWhiteSpace(data.DeliverierId))
            {
                message.AppendError(message, nameof(data.DeliverierId));
            }
            else
            {
                var existDeliverierId = await _repositoryDeliverier.CheckIsUnicById(data.DeliverierId);
                if (!existDeliverierId)
                {
                    message.AppendError(message, nameof(data.DeliverierId), AdditionalMessageEnum.NotFound);
                }
            }
            #endregion

            return _baseInternalServices.BuildMessageValidator(message);
        }

    }
}
