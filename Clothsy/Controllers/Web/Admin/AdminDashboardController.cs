using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Clothsy.Controllers.Admin
{
    [Route("api/admin/dashboard")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            var data = new
            {
                activeHubs = _context.Hubs.Count(h => h.IsActive),
                totalUsers = _context.Users.Count(),
                totalDonations = _context.Donations.Count(),
                itemsDistributed = _context.Donations.Count(d => d.Status == "Distributed")
            };

            return Ok(data);
        }
    }
}
