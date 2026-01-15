using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clothsy.Data;
namespace Clothsy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HubsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/hubs - Get all hubs
        [HttpGet]
        public async Task<IActionResult> GetHubs()
        {
            var hubs = await _context.Hubs
                .Where(h => h.IsActive)
                .Select(h => new
                {
                    h.Id,
                    h.Name,
                    h.Address,
                    h.District,
                    h.Email,
                    h.Phone,
                    h.WorkingHours
                })
                .ToListAsync();

            return Ok(new { success = true, data = hubs });
        }

        // GET: api/hubs/districts - Get all available districts
        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts()
        {
            var districts = await _context.Hubs
                .Where(h => h.IsActive)
                .Select(h => h.District)
                .Distinct()
                .ToListAsync();

            return Ok(new { success = true, data = districts });
        }

        // GET: api/hubs/bydistrict/{district}
        [HttpGet("bydistrict/{district}")]
        public async Task<IActionResult> GetHubByDistrict(string district)
        {
            var hub = await _context.Hubs
                .Where(h => h.District == district && h.IsActive)
                .Select(h => new
                {
                    h.Id,
                    h.Name,
                    h.Address,
                    h.District,
                    h.Email,
                    h.Phone,
                    h.WorkingHours
                })
                .FirstOrDefaultAsync();

            if (hub == null)
                return NotFound(new { success = false, message = "No hub found for this district" });

            return Ok(new { success = true, data = hub });
        }
    }
}