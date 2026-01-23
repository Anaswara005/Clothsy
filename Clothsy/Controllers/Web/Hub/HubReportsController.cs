using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Microsoft.AspNetCore.Mvc;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub/reports")]
    public class HubReportsController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /* =========================================================
           1. SUMMARY CARDS - DISTRICT FILTERED
        ========================================================= */
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var totalDonations = _context.Donations
                .Count(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ DISTRICT FILTER
                );

            var totalRequests = _context.DonationRequests
                .Count(r =>
                    _context.Donations.Any(d =>
                        d.Id == r.DonationId &&
                        d.AssignedHubId == hubId &&
                        d.District == district // ✅ DISTRICT FILTER
                    )
                );

            return Ok(new HubReportSummaryDto
            {
                TotalDonations = totalDonations,
                TotalRequests = totalRequests
            });
        }

        /* =========================================================
           2. MONTHLY DONATION TREND - DISTRICT FILTERED
        ========================================================= */
        [HttpGet("donations/monthly")]
        public IActionResult MonthlyDonations()
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var data = _context.Donations
                .Where(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ DISTRICT FILTER
                )
                .GroupBy(d => new { d.CreatedAt.Year, d.CreatedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .Select(x => new MonthlyCountDto
                {
                    Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"),
                    Count = x.Count
                })
                .ToList();

            return Ok(data);
        }

        /* =========================================================
           3. DONATIONS VS REQUESTS (MONTHLY) - DISTRICT FILTERED
        ========================================================= */
        [HttpGet("donations-vs-requests")]
        public IActionResult DonationsVsRequests()
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var donations = _context.Donations
                .Where(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ DISTRICT FILTER
                )
                .GroupBy(d => new { d.CreatedAt.Year, d.CreatedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .Select(x => new
                {
                    Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"),
                    Count = x.Count
                })
                .ToList();

            var requests = _context.DonationRequests
                .Where(r =>
                    _context.Donations.Any(d =>
                        d.Id == r.DonationId &&
                        d.AssignedHubId == hubId &&
                        d.District == district // ✅ DISTRICT FILTER
                    )
                )
                .GroupBy(r => new { r.RequestedAt.Year, r.RequestedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .Select(x => new
                {
                    Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"),
                    Count = x.Count
                })
                .ToList();

            return Ok(new
            {
                donations,
                requests
            });
        }
    }
}