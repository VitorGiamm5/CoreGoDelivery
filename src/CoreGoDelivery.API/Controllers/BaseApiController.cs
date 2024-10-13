using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseApiController : ControllerBase
    {
        protected new IActionResult Response(ApiResponse result = null)
        {
            if (result?.Data != null && result?.Error == null)
            {
                return StatusCode(201);
            }
            else if (result?.Data == null && result?.Error == null)
            {
                return StatusCode(404, new { message = "Resource not found" });
            }
            else if (result?.Data == null && result?.Error != null)
            {
                return BadRequest(new { message = "Bad request", error = result?.Error });
            }

            return StatusCode(500, new { message = "Unknown error" });
        }
    }
}
