using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Admin
{
    [Route("api/admin/donations")]
    [ApiController]
    public class AdminDonationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Get hubs for dropdown
        [HttpGet("hubs")]
        public async Task<IActionResult> GetHubs()
        {
            try
            {
                var hubs = await _context.Hubs
                    .Where(h => h.IsActive)
                    .Select(h => new
                    {
                        Id = h.Id,
                        Name = h.Name
                    })
                    .OrderBy(h => h.Name)
                    .ToListAsync();

                return Ok(hubs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching hubs", error = ex.Message });
            }
        }

        // 🔹 Get donations (FILTERED)
        [HttpGet]
        public async Task<IActionResult> GetDonations(
            [FromQuery] int hubId,
            [FromQuery] string? status = null,
            [FromQuery] string? range = "all"
        )
        {
            try
            {
                if (hubId <= 0)
                    return BadRequest(new { message = "Hub ID is required" });

                var query = _context.Donations
                    .Include(d => d.Donor)
                    .Where(d => d.AssignedHubId == hubId);

                // Status filter
                if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
                {
                    query = query.Where(d => d.Status.ToLower() == status.ToLower());
                }

                // Date filter
                var now = DateTime.UtcNow;
                if (!string.IsNullOrEmpty(range) && range.ToLower() != "all")
                {
                    DateTime from = range.ToLower() switch
                    {
                        "today" => now.Date,
                        "week" => now.AddDays(-7).Date,
                        "month" => now.AddMonths(-1).Date,
                        _ => DateTime.MinValue
                    };

                    if (from != DateTime.MinValue)
                    {
                        query = query.Where(d => d.CreatedAt >= from);
                    }
                }

                var donations = await query
                    .OrderByDescending(d => d.CreatedAt)
                    .Select(d => new
                    {
                        Id = d.Id,
                        DonationId = d.DonationId ?? $"DON-{d.Id}",
                        Donor = d.Donor != null ? d.Donor.FullName : "Unknown",
                        Item = d.Title ?? "N/A",
                        Category = d.Category ?? "Uncategorized",
                        Date = d.CreatedAt,
                        Status = d.Status ?? "Pending"
                    })
                    .ToListAsync();

                return Ok(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching donations", error = ex.Message });
            }
        }

        // 🔹 Get donation statistics (BONUS)
        [HttpGet("stats/{hubId}")]
        public async Task<IActionResult> GetDonationStats(int hubId)
        {
            try
            {
                if (hubId <= 0)
                    return BadRequest(new { message = "Hub ID is required" });

                var stats = await _context.Donations
                    .Where(d => d.AssignedHubId == hubId)
                    .GroupBy(d => d.Status)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                var total = await _context.Donations
                    .Where(d => d.AssignedHubId == hubId)
                    .CountAsync();

                return Ok(new
                {
                    Total = total,
                    ByStatus = stats
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching statistics", error = ex.Message });
            }
        }
    }
}