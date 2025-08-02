using Microsoft.AspNetCore.Mvc;
using QuickBill.Application;
using QuickBill.Domain;
using QuickBill.Domain.GoogleDTOs;
using QuickBill.Domain.Models.Common;
using System.Net;

namespace QuickBill.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Code = Convert.ToInt32(HttpStatusCode.BadRequest),
                    Message = HttpStatusCode.BadRequest.ToString(),
                    Data = null,
                    Timestamp = DateTime.UtcNow
                });

            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
                return Unauthorized(new ApiResponse<string>
                {
                    Status = "Error",
                    Code = Convert.ToInt32(HttpStatusCode.Unauthorized),
                    Message = "Invalid email or password",
                    Data = null,
                    Timestamp = DateTime.UtcNow
                });

            return Ok(new ApiResponse<LoginResponseDto>
            {
                Status = "success",
                Code = Convert.ToInt32(HttpStatusCode.OK),
                Message = "Login successful",
                Data = result,
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            var result = await _authService.LoginWithGoogleAsync(request.IdToken);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid Google login",
                    Data = null,
                    Timestamp = DateTime.UtcNow
                });

            return Ok(new ApiResponse<GoogleLoginResponseDto>
            {
                Status = HttpStatusCode.OK.ToString(),
                Code = (int)HttpStatusCode.OK,
                Message = "Login successful",
                Data = result,
                Timestamp = DateTime.UtcNow
            });
        }

    }
}
