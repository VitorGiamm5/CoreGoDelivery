using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
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

        public static DeliverierEntity CreateToEntity(DeliverierDto data)
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
                    ImageUrlReference = FileNameNormalize(data)
                }
            };

            return result;
        }

        public static string FileNameNormalize(DeliverierDto data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result;
        }

        public static LicenseTypeEnum ParseLicenseType(DeliverierDto data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

            return licenseType;
        }

        public async Task<string?> BuilderValidatorCreateAsync(DeliverierDto data)
        {
            var message = new StringBuilder();

            BuildMessageCnpj(message, data.CNPJ);

            BuildMessageFullName(data, message);

            BuildMessageBirthDate(data, message);

            BuildMessageLicenseType(data, message);

            await Task.WhenAll(
                 MessageBuilderCreate(data, message),
                 BuildMessageCnh(data, message)
             );

            return BuildMessageValidator(message);
        }

        public async Task MessageBuilderCreate(DeliverierDto data, StringBuilder message)
        {
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.Append($"{nameof(data.Id)}: {data.Id} already exists; ");
                }
            }
        }

        public async Task BuildMessageCnh(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.LicenseNumber))
            {
                message.Append($"Empty: {nameof(data.LicenseNumber)}; ");
            }
            else
            {
                var isValidLicense = CnhValidation.Validate(data.LicenseNumber);

                if (!isValidLicense)
                {
                    message.Append($"{nameof(data.LicenseNumber)}: {data.LicenseNumber} is invalid; ");
                }

                var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(data.LicenseNumber);

                if (!isUnicLicence)
                {
                    message.Append($"{nameof(data.LicenseNumber)}: {data.LicenseNumber} already exists; ");
                }
            }
        }

        public static void BuildMessageCnpj(StringBuilder message, string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                message.Append($"Empty: {nameof(cnpj)}: {cnpj}; ");
            }
            else
            {
                if (!CnpjValidation.Validate(cnpj))
                {
                    message.Append($"{nameof(cnpj)}: {cnpj} is invalid; ");
                }
            }
        }

        public static void BuildMessageFullName(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                message.Append($"Empty: {nameof(data.FullName)}; ");
            }
        }

        public static void BuildMessageBirthDate(DeliverierDto data, StringBuilder message)
        {
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToString()))
            {
                message.Append($"Invalid: {nameof(data.BirthDate)}: {data.BirthDate}; ");
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
                        message.Append("The person must be at least 18 years old.");
                    }
                }
            }
        }

        public static void BuildMessageLicenseType(DeliverierDto data, StringBuilder message)
        {
            if (!Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum _))
            {
                message.Append($"Invalid: {nameof(data.LicenseType)}: {data.LicenseType}; ");
            }
        }
    }
}