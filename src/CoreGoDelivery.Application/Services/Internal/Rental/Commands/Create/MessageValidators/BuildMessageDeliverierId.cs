using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.MessageValidators
{
    public class BuildMessageDeliverierId
    {
        public readonly IDeliverierRepository _repositoryDeliverier;

        public BuildMessageDeliverierId(IDeliverierRepository repositoryDeliverier)
        {
            _repositoryDeliverier = repositoryDeliverier;
        }

        public async Task Build(RentalCreateCommand data, StringBuilder message)
        {
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
        }
    }
}
