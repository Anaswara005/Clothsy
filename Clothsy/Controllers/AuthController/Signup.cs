using Clothsy.DTOs.SignupDTOs;
using Microsoft.AspNetCore.Mvc;
using OtpAuthApi.Services;

namespace OtpAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Initiate signup and send OTP
        /// </summary>
        /// <param name="request">Signup details including full name, phone/email, and contact type</param>
        /// <returns>Success response with contact value where OTP was sent</returns>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            var result = await _userService.SignupAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Verify OTP and complete signup
        /// </summary>
        /// <param name="request">OTP verification details</param>
        /// <returns>Success response with redirect URL</returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var result = await _userService.VerifyOtpAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Resend OTP to the same contact
        /// </summary>
        /// <param name="request">Contact details for OTP resend</param>
        /// <returns>Success response</returns>
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request)
        {
            var result = await _userService.ResendOtpAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}