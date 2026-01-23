using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Clothsy.Models.SignupModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clothsy.Controllers.Web.Hub
{
    [Authorize(Roles = "HUB")]
    [ApiController]
    [Route("api/web/hub/account")]
    public class HubAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public HubAccountController(
            ApplicationDbContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto dto)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return Unauthorized();

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.CurrentPassword
            );

            if (result == PasswordVerificationResult.Failed)
                return BadRequest(new { message = "Current password is incorrect" });

            user.PasswordHash =
                _passwordHasher.HashPassword(user, dto.NewPassword);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully" });
        }
    }
}
