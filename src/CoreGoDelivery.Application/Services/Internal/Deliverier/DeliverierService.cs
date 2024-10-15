using CoreGoDelivery.Application.Services.Internal.Interface;
using System.Text;
using System.Text.RegularExpressions;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;

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
                Id = data.Id ?? Ulid.NewUlid().ToString(),
                FullName = data.FullName,
                CNPJ = Regex.Replace(data.CNPJ, @"[./\s-]", ""),
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = data.LicenseNumber,
                    Type = data.LicenseType,
                    FileNameImageNormalized = FileNameNormalize(data)
                }
            };

            var resultCreate = await _repositoryDeliverier.Create(deliverier);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        private async Task<string?> ValidatorDeliverierAsync(DeliverierDto data)
        {
            var message = new StringBuilder();

            #region Id validator
            if (string.IsNullOrWhiteSpace(data.Id))
            {
                var isUnicId = await _repositoryDeliverier.CheckIsUnicById(data.Id);
                if (!isUnicId) message.Append($"{nameof(data.Id)}: {data.Id} already exists; ");
            }
            #endregion

            #region CNPJ validator
            if (string.IsNullOrWhiteSpace(data.CNPJ))
            {
                message.Append($"Empty: {nameof(data.CNPJ)}: {data.CNPJ}; ");
            }
            else
            {
                var isCnpjValid = CnpjValidation.Validate(data.CNPJ);
                if (!isCnpjValid) message.Append($"{nameof(data.CNPJ)}: {data.CNPJ} is invalid; ");

                var isUnicCnpj = await _repositoryDeliverier.CheckIsUnicByCnpj(data.CNPJ);
                if (!isUnicCnpj) message.Append($"{nameof(data.CNPJ)}: {data.CNPJ} already exists; ");
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
                if (!isValidLicense) message.Append($"{nameof(data.CNPJ)}: {data.CNPJ} is invalid; ");

                var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(data.LicenseNumber);
                if (!isUnicLicence) message.Append($"{nameof(data.LicenseNumber)}: {data.LicenseNumber} already exists; ");
            }
            #endregion

            #region FullName validator
            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                message.Append($"Empty: {nameof(data.FullName)}; ");
            }
            else
            {

            }
            #endregion

            #region BirthDate validator
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToString())) message.Append($"Invalid: {nameof(data.BirthDate)}: {data.BirthDate}; ");
            #endregion

            #region License Type validator
            if (data.LicenseType == LicenceTypeEnum.None) message.Append($"Invalid: {nameof(data.LicenseType)}: {data.LicenseType}; ");
            #endregion

            #region Finaly
            if (message.Length > 0) return message.ToString();
            #endregion

            return null;
        }

        private static string? FinalMessageBuild(bool resultDeliverier, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultDeliverier
                ? null
                : MESSAGE_INVALID_DATA;
        }
        #endregion

        private static string FileNameNormalize(DeliverierDto data)
        {
            return $"CNPJ_{data.CNPJ}.jpg";
        }

        // TODO: HEAVY MISSION
        public async Task<ApiResponse> UploadCnh(string id)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return apiReponse;
        }
    }
}
