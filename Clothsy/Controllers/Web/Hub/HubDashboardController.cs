using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Clothsy.Controllers.Web.Hub
{
    [ApiController]
    [Route("api/web/hub/dashboard")]
    public class HubDashboardController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDashboard([FromQuery] string range = "today")
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ Get district from token

            // 🔒 Hub validation
            if (!_context.Hubs.Any(h => h.Id == hubId && h.IsActive))
                return Unauthorized("Invalid or inactive hub");

            // 🕒 Time window calculation
            var now = DateTime.UtcNow;
            DateTime from;
            DateTime to;

            switch (range.ToLower())
            {
                case "today":
                    // Today: Start of current day to end of current day
                    from = now.Date;
                    to = now.Date.AddDays(1).AddTicks(-1); // 23:59:59.999
                    break;

                case "week":
                    // Week: Most recent Sunday to coming Saturday
                    int daysSinceSunday = ((int)now.DayOfWeek);
                    from = now.Date.AddDays(-daysSinceSunday); // Most recent Sunday
                    to = from.AddDays(7).AddTicks(-1); // Coming Saturday 23:59:59.999
                    break;

                case "month":
                    // Month: First day to last day of current month
                    from = new DateTime(now.Year, now.Month, 1);
                    to = from.AddMonths(1).AddTicks(-1); // Last moment of the month
                    break;

                case "year":
                    // Year: January 1st to December 31st of current year
                    from = new DateTime(now.Year, 1, 1);
                    to = new DateTime(now.Year, 12, 31, 23, 59, 59, 999);
                    break;

                default:
                    from = now.Date;
                    to = now.Date.AddDays(1).AddTicks(-1);
                    break;
            }
            // 📥 Pending Approvals (Waiting or Pending status)
            var pendingApprovals = _context.Donations.Count(d =>
                d.AssignedHubId == hubId &&
                d.District == district && // ✅ Filter by district
                (d.Status == "Waiting" || d.Status == "Pending")
            );

            // ✅ Approved Items
            var approvedItems = _context.Donations.Count(d =>
                d.AssignedHubId == hubId &&
                d.District == district && // ✅ Filter by district
                d.Status == "Approved" &&
                d.ApprovedAt != null &&
                d.ApprovedAt >= from &&
                d.ApprovedAt <= to
            );


            // 📝 Active Requests (Only Pending requests awaiting hub action)
            var activeRequests = _context.DonationRequests.Count(r =>
                r.Status == "Pending" &&
                _context.Donations.Any(d =>
                    d.Id == r.DonationId &&
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ Filter by district
                )
            );
            // 🚚 Total Distributed (Approved and no longer available)
            var totalDistributed = _context.Donations.Count(d =>
                d.AssignedHubId == hubId &&
                d.District == district && // ✅ Filter by district
                d.Status == "Approved" &&
                d.IsAvailable == false &&
                d.ApprovedAt != null &&
                d.ApprovedAt >= from &&
                d.ApprovedAt <= to
            );

            return Ok(new
            {
                range,
                from,
                to,
                pendingApprovals,
                approvedItems,
                activeRequests,
                totalDistributed
            });
        }
    }
}