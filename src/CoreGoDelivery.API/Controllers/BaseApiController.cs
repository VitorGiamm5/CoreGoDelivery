using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreGoDelivery.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseApiController : ControllerBase
    {
        private const string FAULT_ERROR_MESSAGE = "Unknown error";

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
            response.Message = FAULT_ERROR_MESSAGE;

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }
}
