using CoreGoDelivery.Domain.DTO.Response;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal
{
    public class BaseInternalServices
    {
        public const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public static string IdBuild(string? id)
        {
            var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

            return result;
        }

        public static string RemoveCharacteres(string? plateId)
        {
            var result = !string.IsNullOrEmpty(plateId)
                ? Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper()
                : "";
            //TODO: LOGGER
            return result;
        }

        public static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }

    }
}
