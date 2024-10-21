using CoreGoDelivery.Domain.DTO.Response;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Base
{
    public interface IBaseInternalServices
    {
        string IdBuild(string? id);
        string RemoveCharacteres(string? plateId);
        string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse);
        string? BuildMessageValidator(StringBuilder message);
        bool HasMessageError(ApiResponse apiReponse);
        bool RequestIdParamValidator(string? id);
    }
}
