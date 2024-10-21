using CoreGoDelivery.Domain.DTO.Response;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Base
{
    public class BaseInternalServices : IBaseInternalServices
    {
        public const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public string? BuildMessageValidator(StringBuilder message)
        {
            if (message.Length > 0)
            {
                return message.ToString();
            }

            return null;
        }

        public string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }

        public bool HasMessageError(ApiResponse apiReponse)
        {
            var hasMessage = !string.IsNullOrEmpty(apiReponse.Message);

            return hasMessage;
        }

        public string IdBuild(string? id)
        {
            var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

            return result;
        }

        public string RemoveCharacteres(string? plateId)
        {
            var result = !string.IsNullOrEmpty(plateId)
                ? Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper()
                : "";

            return result;
        }

        public bool RequestIdParamValidator(string? id)
        {
            bool isValid = id == ":id" || string.IsNullOrEmpty(id);

            return !isValid;
        }
    }
}
