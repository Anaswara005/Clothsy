using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Admin
{
    [Route("api/admin/analytics")]
    [ApiController]
    public class AdminAnalyticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminAnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics()
        {
            // Stats calculations
            var totalDonations = await _context.Donations.CountAsync();
            var pendingApprovals = await _context.Donations.CountAsync(d => d.Status == "Waiting" || d.Status == "Pending");
            var approvedItems = await _context.Donations.CountAsync(d => d.Status == "Approved");
            var rejectedItems = await _context.Donations.CountAsync(d => d.Status == "Rejected");
            var activeComplaints = await _context.SupportTickets
                .CountAsync(t => t.Status == "Open" || t.Status == "Pending");

            // Donations per hub
            var donationsPerHub = await _context.Donations
                .Where(d => d.AssignedHub != null)
                .GroupBy(d => d.AssignedHubId)
                .Select(g => new
                {
                    hubId = g.Key,
                    count = g.Count()
                })
                .ToListAsync();

            // Get hub names
            var hubIds = donationsPerHub.Select(d => d.hubId).ToList();
            var hubs = await _context.Hubs
                .Where(h => hubIds.Contains(h.Id))
                .Select(h => new { h.Id, h.Name })
                .ToListAsync();

            var donationsPerHubWithNames = donationsPerHub
                .Select(d => new
                {
                    hubName = hubs.FirstOrDefault(h => h.Id == d.hubId)?.Name ?? "Unknown",
                    count = d.count
                })
                .OrderByDescending(x => x.count)
                .ToList();

            // Monthly trend (last 12 months)
            var twelveMonthsAgo = DateTime.UtcNow.AddMonths(-12);
            var monthlyData = await _context.Donations
                .Where(d => d.CreatedAt >= twelveMonthsAgo)
                .GroupBy(d => new { d.CreatedAt.Year, d.CreatedAt.Month })
                .Select(g => new
                {
                    year = g.Key.Year,
                    month = g.Key.Month,
                    count = g.Count()
                })
                .ToListAsync();

            var monthlyTrend = monthlyData
                .OrderBy(x => x.year * 100 + x.month)
                .Select(x => new
                {
                    month = $"{GetMonthName(x.month)} {x.year}",
                    count = x.count
                })
                .ToList();

            // Approval pipeline (same as stats but formatted for chart)
            var approvalPipeline = new
            {
                pending = pendingApprovals,
                approved = approvedItems,
                rejected = rejectedItems
            };

            var response = new
            {
                stats = new
                {
                    totalDonations,
                    pendingApprovals,
                    approvedItems,
                    rejectedItems,
                    activeComplaints
                },
                charts = new
                {
                    donationsPerHub = donationsPerHubWithNames,
                    monthlyTrend,
                    approvalPipeline
                }
            };

            return Ok(response);
        }

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "Jan",
                2 => "Feb",
                3 => "Mar",
                4 => "Apr",
                5 => "May",
                6 => "Jun",
                7 => "Jul",
                8 => "Aug",
                9 => "Sep",
                10 => "Oct",
                11 => "Nov",
                12 => "Dec",
                _ => ""
            };
        }
    }
}
