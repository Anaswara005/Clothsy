using Clothsy.Data;
using Microsoft.AspNetCore.Authorization;
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

                // Donations table = actual donation entries
                totalDonations = _context.Donations.Count(),

                // DonationRequest table = real-world distribution
                itemsCollected = _context.DonationRequests
                    .Count(r => r.Status == "Collected")
            };

            return Ok(data);
        }

    }
}