using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Consts.Regex;
using CoreGoDelivery.Domain.Response;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Base;

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

    public string? FinalMessageBuild(bool resultCreate, ActionResult apiReponse)
    {
        if (apiReponse.HasError())
        {
            return null;
        }

        return resultCreate
            ? null
            : CommomMessagesConst.MESSAGE_INVALID_DATA;
    }

    public string IdBuild(string? id)
    {
        var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

        return result;
    }

    public string RemoveCharacteres(string? plateId)
    {
        if (string.IsNullOrEmpty(plateId))
        {
            return "";
        }

        var result = Regex.Replace(plateId, @RegexCollectionPatterns.SPECIAL_CHARACTER_PATTERN, "").ToUpper();

        return result;
    }

    public bool RequestIdParamValidator(string? id)
    {
        bool isValid = id == ":id" || string.IsNullOrEmpty(id);

        return !isValid;
    }
}
