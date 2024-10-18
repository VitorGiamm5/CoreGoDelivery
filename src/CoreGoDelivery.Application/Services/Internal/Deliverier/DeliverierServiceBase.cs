using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierServiceBase : BaseInternalServices
    {
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly ILicenceDriverRepository _repositoryLicence;

        public DeliverierServiceBase(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence)
        {
            _repositoryDeliverier = repositoryDeliverier;
            _repositoryLicence = repositoryLicence;
        }

        #region Mappers

        public static DeliverierEntity MapCreateToEntity(DeliverierDto data)
        {
            var result = new DeliverierEntity()
            {
                Id = IdBuild(data.Id),
                FullName = data.FullName,
                CNPJ = RemoveCharacteres(data.CNPJ),
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
        public async Task<string?> BuilderValidatorCreateAsync(DeliverierDto data)
        {
            var message = new StringBuilder();

            BuildMessageCnpj(message, data.CNPJ);

            BuildMessageFullName(data, message);

            BuildMessageBirthDate(data, message);

            BuildMessageLicenseType(data, message);

            await Task.WhenAll(
                 BuildMessageCreate(data, message),
                 BuildMessageCnh(data, message)
             );

            return BuildMessageValidator(message);
        }

        public static void BuildMessageCnpj(StringBuilder message, string cnpj)
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
        }

        public static void BuildMessageFullName(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                message.AppendError(message, data.FullName);
            }
        }

        public static void BuildMessageBirthDate(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToString()))
            {
                message.AppendError(message, data.BirthDate);
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

        public static void BuildMessageLicenseType(DeliverierDto data, StringBuilder message)
        {
            if (!Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum _))
            {
                message.AppendError(message, data.LicenseType);
            }
        }

        public async Task BuildMessageCreate(DeliverierDto data, StringBuilder message)
        {
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.AppendError(message, data.Id, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }

        public async Task BuildMessageCnh(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.LicenseNumber))
            {
                message.AppendError(message, data.LicenseNumber);
            }
            else
            {
                var isValidLicense = CnhValidation.Validate(data.LicenseNumber);

                if (!isValidLicense)
                {
                    message.AppendError(message, data.LicenseNumber);
                }

                var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(data.LicenseNumber);

                if (!isUnicLicence)
                {
                    message.AppendError(message, data.LicenseNumber, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }

        #endregion

        #region Features to Mappers

        public static string FileNameLicenseNormalize(DeliverierDto data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result;
        }

        public static LicenseTypeEnum ParseLicenseType(DeliverierDto data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

            return licenseType;
        }

        #endregion
    }
}