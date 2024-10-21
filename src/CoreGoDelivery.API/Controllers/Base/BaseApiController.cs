using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreGoDelivery.Api.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseApiController : ControllerBase
    {

        protected new IActionResult Response(object? result)
        {
            throw new NotImplementedException();
        }

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
            response.Message = CommomMessagesService.MESSAGE_INVALID_DATA;

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }
}
