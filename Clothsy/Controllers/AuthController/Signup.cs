using Clothsy.Data;
using Clothsy.DTOs.SignupDTOs;
using Clothsy.Models.SignupModels;
using Clothsy.Services.AuthServices.Signup_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtpAuthApi.Services;

namespace ClothsyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthController(
            ApplicationDbContext context,
            IPasswordService passwordService,
            ITokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        // POST: api/auth/signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Check if phone number already exists
            var existingPhoneUser = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            if (existingPhoneUser != null) 
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Phone number already registered"
                });
            }

            // Check if email already exists
            var existingEmailUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingEmailUser != null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Email already registered"
                });
            }

            // Hash the password
            var passwordHash = _passwordService.HashPassword(request.Password);

            // Create new user
            var user = new User
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                District = request.District,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _tokenService.GenerateToken(user.Id, user.Email);

            return Ok(new
            {
                success = true,
                message = "Signup successful",
                token = token,
                userId = user.Id
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Find user by phone number or email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneOrEmail || u.Email == request.PhoneOrEmail);

            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid credentials"
                });
            }

            // Verify password
            if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid credentials"
                });
            }

            // Generate JWT token
            var token = _tokenService.GenerateToken(user.Id, user.Email);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                token = token,
                userId = user.Id
            });
        }

        // GET: api/auth/validate-token
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Ok(new { valid = false });
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var principal = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                return Ok(new { valid = false });
            }

            return Ok(new { valid = true });
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // In JWT, logout is handled on client side by removing the token
            // This endpoint is provided for consistency but doesn't need to do anything server-side
            return Ok(new
            {
                success = true,
                message = "Logout successful"
            });
        }
        [HttpPost("verify-user")]
        public async Task<IActionResult> VerifyUser([FromBody] VerifyUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Check if user exists with provided email and phone
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PhoneNumber == request.PhoneNumber);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No account found with this email and phone number"
                });
            }

            return Ok(new
            {
                success = true,
                message = "User verified successfully"
            });
        }

        // POST: api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Find user
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PhoneNumber == request.PhoneNumber);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            // Hash new password
            user.PasswordHash = _passwordService.HashPassword(request.NewPassword);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Password reset successful"
            });
        }
    }
}