using Clothsy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Admin
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/admin/users?district=Kollam
        [HttpGet]
        public async Task<IActionResult> GetUsersByDistrict([FromQuery] string district)
        {
            if (string.IsNullOrWhiteSpace(district))
                return BadRequest("District is required");

            var users = await _context.Users
                .Where(u => u.District == district)
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new
                {
                    u.Id,
                    Name = u.FullName,   // ✅ FIXED
                    u.Email,
                    u.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
