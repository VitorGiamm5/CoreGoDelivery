using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create
{
    public class DeliverierCreateValidator
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly BuildMessageCnpj _buildMessageCnpj;
        public readonly BuildMessageFullName _buildMessageFullName;
        public readonly BuildMessageBirthDate _buildMessageBirthDate;
        public readonly BuildMessageLicenseType _buildMessageLicenseType;
        public readonly BuildMessageDeliverierCreate _buildMessageCreate;
        public readonly BuildMessageCnh _buildMessageCnh;

        public DeliverierCreateValidator(
            IBaseInternalServices baseInternalServices,
            BuildMessageCnpj buildMessageCnpj,
            BuildMessageFullName buildMessageFullName,
            BuildMessageBirthDate buildMessageBirthDate,
            BuildMessageLicenseType buildMessageLicenseType,
            BuildMessageDeliverierCreate buildMessageCreate,
            BuildMessageCnh buildMessageCnh)
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
}
