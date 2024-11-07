using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateValidator
{
    public readonly IBaseInternalServices _baseInternalServices;

    public readonly DeliverierBuildMessageCnpj _buildMessageCnpj;
    public readonly DeliverierBuildMessageBirthDate _buildMessageBirthDate;
    public readonly DeliverierBuildMessageDeliverierCreate _buildMessageCreate;
    public readonly DeliverierBuildMessageCnh _buildMessageCnh;

    public DeliverierCreateValidator(
        IBaseInternalServices baseInternalServices,
        DeliverierBuildMessageCnpj buildMessageCnpj,
        DeliverierBuildMessageBirthDate buildMessageBirthDate,
        DeliverierBuildMessageDeliverierCreate buildMessageCreate,
        DeliverierBuildMessageCnh buildMessageCnh)
    {
        _baseInternalServices = baseInternalServices;
        _buildMessageCnpj = buildMessageCnpj;
        _buildMessageBirthDate = buildMessageBirthDate;
        _buildMessageCreate = buildMessageCreate;
        _buildMessageCnh = buildMessageCnh;
    }

    public async Task<StringBuilder?> Validator(DeliverierCreateCommand data)
    {
        var message = new StringBuilder();

        _buildMessageCnpj.Build(message, data.Cnpj);

        DeliverierBuildMessageFullName.Build(data, message);

        DeliverierBuildMessageBirthDate.Build(data, message);

        DeliverierBuildMessageLicenseType.Build(data, message);

        await _buildMessageCreate.Build(data, message);

        await _buildMessageCnh.Build(data, message);

        return message;
    }
}
