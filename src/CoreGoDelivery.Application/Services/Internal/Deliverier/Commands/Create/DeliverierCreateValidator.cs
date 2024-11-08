using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateValidator
{
    public readonly DeliverierBuildMessageDeliverierCreate _buildMessageCreate;
    public readonly DeliverierBuildMessageCnh _buildMessageCnh;

    public DeliverierCreateValidator(
        DeliverierBuildMessageDeliverierCreate buildMessageCreate,
        DeliverierBuildMessageCnh buildMessageCnh)
    {
        _buildMessageCreate = buildMessageCreate;
        _buildMessageCnh = buildMessageCnh;
    }

    public async Task<StringBuilder> Validate(DeliverierCreateCommand data)
    {
        var message = new StringBuilder();

        DeliverierBuildMessageCnpj.Build(message, data.Cnpj);

        DeliverierBuildMessageFullName.Build(data, message);

        DeliverierBuildMessageBirthDate.Build(data, message);

        DeliverierBuildMessageLicenseType.Build(data, message);

        await _buildMessageCreate.Build(data, message);

        await _buildMessageCnh.Build(data, message);

        return message;
    }
}
