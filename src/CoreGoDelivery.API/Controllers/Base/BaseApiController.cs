using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ActionResult = CoreGoDelivery.Domain.Response.ActionResult;

namespace CoreGoDelivery.Api.Controllers.Base;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class BaseApiController : ControllerBase
{
    protected new IActionResult Response(ActionResult response)
    {
        if (response.HasError())
        {
            if (response.HasDataType())
            {
                return StatusCode((int)HttpStatusCode.NotFound, response);
            }

            response.Data = null;

            return StatusCode((int)HttpStatusCode.BadRequest, response);
        }
        else if (!response.HasDataType())
        {
            return StatusCode((int)HttpStatusCode.Created);
        }
        else if (response.HasDataType())
        {
            return StatusCode((int)HttpStatusCode.OK, response.Data);
        }

        response.Data = null;
        response.SetMessage(CommomMessagesConst.MESSAGE_INVALID_DATA);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);
    }

    public static string IdBuild(string? id)
    {
        var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

        return result;
    }


    protected IActionResult? IdParamValidator(string? id)
    {
        bool isValid = id != ":id" || !string.IsNullOrEmpty(id);

        if (!isValid)
        {
            var result = new ActionResult();

            result.SetMessage("id param".AppendError());

            return StatusCode((int)HttpStatusCode.InternalServerError, result);
        }

        return null;
    }
}
