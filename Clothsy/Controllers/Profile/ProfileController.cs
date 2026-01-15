using Clothsy.Data;
using Clothsy.DTOs.Profile;
using ClothsyAPI.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ClothsyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        // GET: api/profile
        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetProfile()
        {
            try
            {
                var userId = GetUserId();
                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => new
                    {
                        u.Id,
                        u.FullName,
                        u.Email,
                        u.PhoneNumber,
                        u.CreatedAt
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(new ApiResponse<object>(false, "User not found", null));
                }

                return Ok(new ApiResponse<object>(true, "Profile retrieved successfully", user));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> UpdateProfile(
     [FromBody] UpdateProfileRequest request)
        {
            try
            {
                var userId = GetUserId();
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return NotFound(new ApiResponse<object>(false, "User not found", null));
                }

                // 🔐 Validation (same rules as signup)
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return BadRequest(new ApiResponse<object>(false, "Name is required", null));
                }

                if (!Regex.IsMatch(request.Email,
                    @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    return BadRequest(new ApiResponse<object>(false, "Invalid email format", null));
                }

                if (!Regex.IsMatch(request.PhoneNumber,
                    @"^[6-9]\d{9}$"))
                {
                    return BadRequest(new ApiResponse<object>(false, "Invalid phone number", null));
                }

                // ❌ Email uniqueness
                if (await _context.Users.AnyAsync(u =>
                    u.Email == request.Email && u.Id != userId))
                {
                    return BadRequest(new ApiResponse<object>(false, "Email already in use", null));
                }

                // ❌ Phone uniqueness
                if (await _context.Users.AnyAsync(u =>
                    u.PhoneNumber == request.PhoneNumber && u.Id != userId))
                {
                    return BadRequest(new ApiResponse<object>(false, "Phone number already in use", null));
                }

                // ✅ Update
                user.FullName = request.FullName.Trim();
                user.Email = request.Email.Trim();
                user.PhoneNumber = request.PhoneNumber.Trim();

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>(true, "Profile updated successfully", new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.PhoneNumber
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponse<object>(false, ex.Message, null));
            }
        }

        // DELETE: api/profile/account
        [HttpDelete("account")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAccount(
     DeleteAccountRequest request)
        {
            try
            {
                var userId = GetUserId();
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                    return NotFound(new ApiResponse<object>(false, "User not found", null));

                // 🔐 Verify password
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return Unauthorized(new ApiResponse<object>(false, "Invalid password", null));

                // ===============================
                // 🔥 MANUAL DELETE (ORDER MATTERS)
                // ===============================

                // 1️⃣ Requests made by user
                var requests = await _context.Requests
                    .Where(r => r.RequesterId == userId)
                    .ToListAsync();
                _context.Requests.RemoveRange(requests);

                // 2️⃣ Donations by user
                var donations = await _context.Donations
                    .Where(d => d.DonorUserId == userId)
                    .ToListAsync();
                _context.Donations.RemoveRange(donations);
                // DonationImages auto-delete via cascade

                // 3️⃣ Support tickets
                var tickets = await _context.SupportTickets
                    .Where(t => t.UserId == userId)
                    .ToListAsync();
                _context.SupportTickets.RemoveRange(tickets);

                // 4️⃣ Addresses auto-delete via cascade

                // 5️⃣ FINALLY delete user
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>(true, "Account deleted successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponse<object>(false, ex.InnerException?.Message ?? ex.Message, null));
            }
        }

        [HttpPost("verify-password")]
        public async Task<ActionResult<ApiResponse<object>>> VerifyPassword(
    [FromBody] VerifyPasswordDto request)
        {
            try
            {
                var userId = GetUserId();
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return Unauthorized(
                        new ApiResponse<object>(false, "User not found", null));
                }

                // ✅ VERIFY PASSWORD USING BCrypt (MATCHES DELETE)
                var isValid = BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash
                );

                if (!isValid)
                {
                    return Ok(
                        new ApiResponse<object>(false, "Incorrect password", null));
                }

                return Ok(
                    new ApiResponse<object>(true, "Password verified", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponse<object>(false, ex.Message, null));
            }
        }

    }
}