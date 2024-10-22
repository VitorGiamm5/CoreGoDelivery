using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators
{
    public class BuildMessageDeliverierCreate
    {
        public readonly IDeliverierRepository _repositoryDeliverier;

        public BuildMessageDeliverierCreate(IDeliverierRepository repositoryDeliverier)
        {
            _repositoryDeliverier = repositoryDeliverier;
        }

        public async Task Build(DeliverierCreateCommand data, StringBuilder message)
        {
            var paramName = "idDeliverier";

            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }
    }
}
