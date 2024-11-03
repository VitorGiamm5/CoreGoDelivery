using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateValidator
{
    public readonly IBaseInternalServices _baseInternalServices;

    public readonly DeliverierBuildMessageCnpj _buildMessageCnpj;
    public readonly DeliverierBuildMessageFullName _buildMessageFullName;
    public readonly DeliverierBuildMessageBirthDate _buildMessageBirthDate;
    public readonly DeliverierBuildMessageLicenseType _buildMessageLicenseType;
    public readonly DeliverierBuildMessageDeliverierCreate _buildMessageCreate;
    public readonly DeliverierBuildMessageCnh _buildMessageCnh;

    public DeliverierCreateValidator(
        IBaseInternalServices baseInternalServices,
        DeliverierBuildMessageCnpj buildMessageCnpj,
        DeliverierBuildMessageFullName buildMessageFullName,
        DeliverierBuildMessageBirthDate buildMessageBirthDate,
        DeliverierBuildMessageLicenseType buildMessageLicenseType,
        DeliverierBuildMessageDeliverierCreate buildMessageCreate,
        DeliverierBuildMessageCnh buildMessageCnh)
    {
        _baseInternalServices = baseInternalServices;
        _buildMessageCnpj = buildMessageCnpj;
        _buildMessageFullName = buildMessageFullName;
        _buildMessageBirthDate = buildMessageBirthDate;
        _buildMessageLicenseType = buildMessageLicenseType;
        _buildMessageCreate = buildMessageCreate;
        _buildMessageCnh = buildMessageCnh;
    }

    public async Task<string?> Validator(DeliverierCreateCommand data)
    {
        var message = new StringBuilder();

        _buildMessageCnpj.Build(message, data.Cnpj);

        _buildMessageFullName.Build(data, message);

        _buildMessageBirthDate.Build(data, message);

        _buildMessageLicenseType.Build(data, message);

        await _buildMessageCreate.Build(data, message);

        await _buildMessageCnh.Build(data, message);

        return _baseInternalServices.BuildMessageValidator(message);
    }
}
