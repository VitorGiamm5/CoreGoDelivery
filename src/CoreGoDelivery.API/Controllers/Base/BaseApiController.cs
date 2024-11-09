using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
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
            return StatusCode((int)HttpStatusCode.BadRequest, response.GetError());
        }
        else if (response.HasData())
        {
            return StatusCode((int)HttpStatusCode.OK, response.GetData());
        }

        response.SetError(CommomMessagesConst.MESSAGE_INVALID_DATA);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);
    }

    protected IActionResult ResponseError(object exception)
    {
        var apiResponse = new ActionResult();

        apiResponse.SetError(CommomMessagesConst.MESSAGE_INVALID_DATA, exception);

        return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
    }

    public static string IdBuild(string? id)
    {
        var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

        return result;
    }

    public static string IdBuild()
    {
        return Ulid.NewUlid().ToString();
    }

    protected static string? IdParamValidator(string? id)
    {
        bool isNotValid = id == ":id" || string.IsNullOrEmpty(id);

        if (isNotValid)
        {
            return "id param".AppendError();
        }

        return null;
    }
}
