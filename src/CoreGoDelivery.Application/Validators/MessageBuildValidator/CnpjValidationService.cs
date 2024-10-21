using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Validators.Interfaces;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Validators.MessageBuildValidator
{
    public class CnpjValidationService : IMessageBuildValidator
    {
        public string? MessageBuilderCnpj(StringBuilder message, string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                message.AppendError(message, cnpj, AdditionalMessageEnum.None);
            }
            else
            {
                if (!CnpjValidation.Validate(cnpj))
                {
                    message.AppendError(message, cnpj, AdditionalMessageEnum.None);
                }
            }

            return null;
        }
    }
}
