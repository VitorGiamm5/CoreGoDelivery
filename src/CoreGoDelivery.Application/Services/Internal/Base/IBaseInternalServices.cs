using CoreGoDelivery.Domain.Response;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Base
{
    public interface IBaseInternalServices
    {
        string IdBuild(string? id);
        string RemoveCharacteres(string? plateId);
        string? FinalMessageBuild(bool resultCreate, ActionResult apiReponse);
        string? BuildMessageValidator(StringBuilder message);
        bool RequestIdParamValidator(string? id);
    }
}
