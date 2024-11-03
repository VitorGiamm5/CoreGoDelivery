using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;

public class RentalCreateValidate
{
    public readonly IBaseInternalServices _baseInternalServices;

    public readonly RentalBuildMessagePlanId _buildMessagePlanId;
    public readonly RentalBuildMessageMotorcycleId _buildMessageMotorcycleId;
    public readonly RentalBuildMessageDeliverierId _buildMessageDeliverierId;

    public RentalCreateValidate(
        IBaseInternalServices baseInternalServices,
        RentalBuildMessagePlanId buildMessagePlanId,
        RentalBuildMessageMotorcycleId buildMessageMotorcycleId,
        RentalBuildMessageDeliverierId buildMessageDeliverierId)
    {
        _baseInternalServices = baseInternalServices;
        _buildMessagePlanId = buildMessagePlanId;
        _buildMessageMotorcycleId = buildMessageMotorcycleId;
        _buildMessageDeliverierId = buildMessageDeliverierId;
    }

    public async Task<string?> BuilderCreateValidator(RentalCreateCommand data)
    {
        var message = new StringBuilder();

        var paramName = nameof(data.PlanId);

        await _buildMessagePlanId.Build(data, message, paramName);

        await _buildMessageMotorcycleId.Build(data, message, paramName);

        await _buildMessageDeliverierId.Build(data, message);

        return _baseInternalServices.BuildMessageValidator(message);
    }
}
