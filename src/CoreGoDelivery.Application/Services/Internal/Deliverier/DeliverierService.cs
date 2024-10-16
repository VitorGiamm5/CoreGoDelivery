using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierService : IDeliverierService
    {
        private readonly IDeliverierRepository _repositoryDeliverier;
        private readonly ILicenceDriverRepository _repositoryLicence;
        private const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public DeliverierService(
            IDeliverierRepository repositoryDeliverier,
            ILicenceDriverRepository repositoryLicence)
        {
            _repositoryDeliverier = repositoryDeliverier;
            _repositoryLicence = repositoryLicence;
        }

        #region Create
        public async Task<ApiResponse> Create(DeliverierDto data)
        {

            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await ValidatorDeliverierAsync(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var deliverier = new DeliverierEntity()
            {
                Id = SelectId(data.Id),
                FullName = data.FullName,
                CNPJ = CnpjNormalize(data),
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = data.LicenseNumber,
                    Type = ParseLicenseType(data),
                    FileNameImageNormalized = FileNameNormalize(data)
                }
            };

            var resultCreate = await _repositoryDeliverier.Create(deliverier);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        private string SelectId(string id)
        {
            var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

            return result;
        }

        private static LicenceTypeEnum ParseLicenseType(DeliverierDto data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenceTypeEnum licenseType);

            return licenseType;
        }

        private static string CnpjNormalize(DeliverierDto data)
        {
            return Regex.Replace(data.CNPJ, @"[./\s-]", "");
        }

        private async Task<string?> ValidatorDeliverierAsync(DeliverierDto data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (!string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);

                if (!isUnicId)
                {
                    message.Append($"{nameof(data.Id)}: {data.Id} already exists; ");
                }
            }
            #endregion

            #region CNPJ validator
            if (string.IsNullOrWhiteSpace(data.CNPJ))
            {
                message.Append($"Empty: {nameof(data.CNPJ)}: {data.CNPJ}; ");
            }
            else
            {
                if (!CnpjValidation.Validate(data.CNPJ))
                {
                    message.Append($"{nameof(data.CNPJ)}: {data.CNPJ} is invalid; ");
                }
            }
            #endregion

            #region LicenseNumber validator
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
            #endregion

            #region FullName validator
            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                message.Append($"Empty: {nameof(data.FullName)}; ");
            }
            #endregion

            #region BirthDate validator
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
            #endregion

            #region License Type validator
            if (!Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenceTypeEnum _))
            {
                message.Append($"Invalid: {nameof(data.LicenseType)}: {data.LicenseType}; ");
            }
            #endregion

            #region Finaly
            if (message.Length > 0)
            {
                return message.ToString();
            }
            #endregion

            return null;
        }

        private static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }
        #endregion

        private static string FileNameNormalize(DeliverierDto data)
        {
            return $"CNH_{data.LicenseNumber}.png";
        }

        public async Task<ApiResponse> UploadCnh(string id)
        {
            // TODO: HEAVY MISSION IMAGE FILE
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return apiReponse;
        }
    }
}
