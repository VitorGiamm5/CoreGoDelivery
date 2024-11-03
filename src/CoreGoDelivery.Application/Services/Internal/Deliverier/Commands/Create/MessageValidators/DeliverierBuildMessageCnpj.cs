using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;

public class DeliverierBuildMessageCnpj
{
    public void Build(StringBuilder message, string cnpj)
    {
        var paramName = nameof(cnpj);

        if (string.IsNullOrWhiteSpace(cnpj))
        {
            message.AppendError(message, paramName);
        }
        else
        {
            if (!CnpjValidation.Validate(cnpj))
            {
                message.AppendError(message, paramName, AdditionalMessageEnum.InvalidFormat);
            }
        }
    }
}
