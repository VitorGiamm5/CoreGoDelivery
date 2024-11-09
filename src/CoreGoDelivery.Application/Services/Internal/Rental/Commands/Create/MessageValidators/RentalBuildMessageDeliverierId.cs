using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators;

public class RentalBuildMessageDeliverierId
{
    public readonly IDeliverierRepository _repositoryDeliverier;

    public RentalBuildMessageDeliverierId(IDeliverierRepository repositoryDeliverier)
    {
        _repositoryDeliverier = repositoryDeliverier;
    }

    public async Task Build(RentalCreateCommand data, StringBuilder message)
    {
        if (string.IsNullOrWhiteSpace(data.DeliverierId))
        {
            message.Append(nameof(data.DeliverierId));
        }
        else
        {
            var existDeliverierId = await _repositoryDeliverier.GetOneById(data.DeliverierId);

            if (existDeliverierId == null)
            {
                message.Append(nameof(data.DeliverierId).AppendError(AdditionalMessageEnum.NotFound));
            }
        }
    }
}
