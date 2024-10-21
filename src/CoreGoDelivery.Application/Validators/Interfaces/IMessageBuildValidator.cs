using System.Text;

namespace CoreGoDelivery.Application.Validators.Interfaces
{
    public interface IMessageBuildValidator
    {
        string? MessageBuilderCnpj(StringBuilder message, string cnpj);
    }
}
