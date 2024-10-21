using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands
{
    public class DeliverierServiceBase
    {
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly ILicenceDriverRepository _repositoryLicence;
        public readonly IBaseInternalServices _baseInternalServices;

        public DeliverierServiceBase(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence,
            IBaseInternalServices baseInternalServices)
        {
            _repositoryDeliverier = repositoryDeliverier;
            _repositoryLicence = repositoryLicence;
            _baseInternalServices = baseInternalServices;
        }

        #region Mappers

        public DeliverierEntity MapCreateToEntity(DeliverierCreateCommand data)
        {
            var result = new DeliverierEntity()
            {
                Id = _baseInternalServices.IdBuild(data.Id),
                FullName = data.FullName,
                CNPJ = _baseInternalServices.RemoveCharacteres(data.Cnpj),
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = data.LicenseNumber,
                    Type = ParseLicenseType(data),
                    ImageUrlReference = FileNameLicenseNormalize(data)
                }
            };

            return result;
        }

        #endregion

        #region Create validator
        public async Task<string?> BuilderValidatorCreateAsync(DeliverierCreateCommand data)
        {
            var message = new StringBuilder();

            BuildMessageCnpj(message, data.Cnpj);

            BuildMessageFullName(data, message);

            BuildMessageBirthDate(data, message);

            BuildMessageLicenseType(data, message);

            await Task.WhenAll(
                 BuildMessageCreate(data, message),
                 BuildMessageCnh(data, message)
             );

            return _baseInternalServices.BuildMessageValidator(message);
        }

        public static void BuildMessageCnpj(StringBuilder message, string cnpj)
        {
            var paramName = nameof(cnpj);

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                message.AppendError(message, paramName, AdditionalMessageEnum.None);
            }
            else
            {
                if (!CnpjValidation.Validate(cnpj))
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.None);
                }
            }
        }

        public static void BuildMessageFullName(DeliverierCreateCommand data, StringBuilder message)
        {
            var paramName = nameof(data.FullName);

            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                message.AppendError(message, paramName);
            }
        }

        public static void BuildMessageBirthDate(DeliverierCreateCommand data, StringBuilder message)
        {
            var paramName = nameof(data.BirthDate);

            if (string.IsNullOrWhiteSpace(data.BirthDate.ToString()))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                if (data.BirthDate is DateTime birthDate)
                {
                    var age = DateTime.Today.Year - birthDate.Year;

                    if (birthDate.Date > DateTime.Today.AddYears(-age))
                    {
                        age--;
                    }

                    if (age < 18)
                    {
                        message.AppendLine("The person must be at least 18 years old.");
                    }
                }
            }
        }

        public static void BuildMessageLicenseType(DeliverierCreateCommand data, StringBuilder message)
        {
            var paramName = nameof(data.LicenseType);

            if (!Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum _))
            {
                message.AppendError(message, paramName);
            }
        }

        public async Task BuildMessageCreate(DeliverierCreateCommand data, StringBuilder message)
        {
            var paramName = "idDaliverier";

            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }

        public async Task BuildMessageCnh(DeliverierCreateCommand data, StringBuilder message)
        {
            var licenseNumber = data.LicenseNumber;
            var paramName = nameof(licenseNumber);

            if (string.IsNullOrWhiteSpace(licenseNumber))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                var isValidLicense = CnhValidation.Validate(licenseNumber);

                if (!isValidLicense)
                {
                    message.AppendError(message, paramName);
                }

                var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(licenseNumber);

                if (!isUnicLicence)
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }

        #endregion

        #region Features to Mappers

        public static string FileNameLicenseNormalize(DeliverierCreateCommand data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result;
        }

        public static LicenseTypeEnum ParseLicenseType(DeliverierCreateCommand data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

            return licenseType;
        }

        #endregion
    }
}