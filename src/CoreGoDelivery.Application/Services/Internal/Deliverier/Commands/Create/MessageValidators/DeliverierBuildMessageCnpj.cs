using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;

public static class DeliverierBuildMessageCnpj
{
    public static void Build(StringBuilder message, string cnpj)
    {
        var paramName = nameof(cnpj);

        if (string.IsNullOrWhiteSpace(cnpj))
        {
            message.Append(paramName.AppendError());
        }
        else
        {
            if (!CnpjValidation.Validate(cnpj))
            {
                message.Append(paramName.AppendError(AdditionalMessageEnum.InvalidFormat));
            }
        }
    }
}
