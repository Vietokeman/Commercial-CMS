using CMS.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult SuccessResponse<T>(T data, string message = "Success")
        {
            return Ok(ApiResponse<T>.SuccessResult(data, message));
        }

        protected IActionResult SuccessResponse(string message = "Success")
        {
            return Ok(ApiResponse<object>.SuccessResult(null, message));
        }

        protected IActionResult ErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, ApiResponse<object>.FailureResult(message));
        }

        protected IActionResult ErrorResponse(string message, List<string> errors, int statusCode = 400)
        {
            return StatusCode(statusCode, ApiResponse<object>.FailureResult(message, errors));
        }

        protected IActionResult NotFoundResponse(string message = "Resource not found")
        {
            return NotFound(ApiResponse<object>.FailureResult(message));
        }

        protected IActionResult BadRequestResponse(string message = "Bad request")
        {
            return BadRequest(ApiResponse<object>.FailureResult(message));
        }

        protected IActionResult UnauthorizedResponse(string message = "Unauthorized")
        {
            return Unauthorized(ApiResponse<object>.FailureResult(message));
        }
    }
}
