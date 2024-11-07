using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreGoDelivery.Api.Controllers.Base;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api")]
public class BaseApiController : ControllerBase
{
    protected new IActionResult Response(ApiResponse response)
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
        response.Message = CommomMessagesConst.MESSAGE_INVALID_DATA;

        return StatusCode((int)HttpStatusCode.InternalServerError, response);
    }

    public static string IdBuild(string? id)
    {
        var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

        return result;
    }

    public void ParamIdValidator(string? id)
    {
        bool isValid = id == ":id" || string.IsNullOrEmpty(id);

        if (isValid)
        {
            var result = new ApiResponse { Data = null, Message = "Error: invalid id param" };

            Response(result);
        }
    }
}
