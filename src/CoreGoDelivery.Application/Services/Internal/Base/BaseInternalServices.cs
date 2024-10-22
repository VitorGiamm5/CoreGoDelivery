using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Consts.Regex;
using CoreGoDelivery.Domain.Response;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Base
{
    public class BaseInternalServices : IBaseInternalServices
    {
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
                : CommomMessagesConst.MESSAGE_INVALID_DATA;
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

        public string RemoveCharacteres(string? plate)
        {
            if (string.IsNullOrEmpty(plate))
            {
                return "";
            }

            var result = Regex.Replace(plate, @RegexCollectionPatterns.SPECIAL_CHARACTER_PATTERN, "").ToUpper();

            return result;
        }

        public bool RequestIdParamValidator(string? id)
        {
            bool isValid = id == ":id" || string.IsNullOrEmpty(id);

            return !isValid;
        }
    }
}
