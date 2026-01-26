using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub/requests")]
    public class HubRequestsController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /* =========================================================
           GET : All requests for logged-in hub - DISTRICT FILTERED
        ========================================================= */
        [HttpGet("pending")]
        public IActionResult GetPendingRequests()

        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var requests = _context.DonationRequests
                .Include(r => r.Donation)
                .Include(r => r.Requester)
                .Where(r =>
    r.Donation != null &&
    r.Donation.AssignedHubId == hubId &&
    r.Donation.District == district &&
    (r.Status == "Pending" || r.Status == "Approved")
)

                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new HubRequestDto
                {
                    RequestId = !string.IsNullOrEmpty(r.RequestId) ? r.RequestId : $"REQ-{r.Id}", // 👈 Handle legacy data
                    DonationId = r.DonationId,
                    DonationCode = r.Donation!.DonationId,
                    ItemTitle = r.Donation.Title,
                    Category = r.Donation.Category,
                    RequesterName = r.Requester != null ? r.Requester.FullName : "Unknown",
                    RequesterPhone = r.Requester != null ? r.Requester.PhoneNumber : "",
                    District = r.Donation.District,
                    RequestedAt = r.RequestedAt,
                    RejectionReason = r.RejectionReason,
                    Status = r.Status,
                    ThumbnailImageUrl = r.Donation.ThumbnailImageUrl
                })
                .ToList();

            return Ok(requests);
        }

        /* =========================================================
           POST : Approve request - DISTRICT SECURED
        ========================================================= */
        [HttpPost("{id}/approve")]
        public IActionResult ApproveRequest(string id)
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var request = _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefault(r =>
                    r.RequestId == id && // 👈 Query by RequestId
                    r.Donation != null &&
                    r.Donation.AssignedHubId == hubId &&
                    r.Donation.District == district // ✅ DISTRICT SECURITY CHECK
                );

            if (request == null)
                return NotFound("Request not found for this hub");

            if (request.Status != "Pending")
                return BadRequest("Request already processed");

            request.Status = "Approved";
            request.ApprovedAt = DateTime.UtcNow;

            if (request.Donation != null)
                request.Donation.IsAvailable = false;

            _context.SaveChanges();

            return Ok(new { message = "Request approved" });
        }

        /* =========================================================
           POST : Reject request - DISTRICT SECURED
        ========================================================= */
        [HttpPost("{id}/reject")]
        public IActionResult RejectRequest(string id, [FromBody] RejectRequestDto dto)
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var request = _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefault(r =>
                    r.RequestId == id && // 👈 Query by RequestId
                    r.Donation != null &&
                    r.Donation.AssignedHubId == hubId &&
                    r.Donation.District == district // ✅ DISTRICT SECURITY CHECK
                );

            if (request == null)
                return NotFound("Request not found for this hub");

            if (request.Status != "Pending")
                return BadRequest("Request already processed");

            request.Status = "Rejected";
            request.RejectedAt = DateTime.UtcNow;
            request.RejectionReason = dto.Reason;


            _context.SaveChanges();

            return Ok(new { message = "Request rejected" });
        }

        /* =========================================================
           GET : Images for request's donation - DISTRICT SECURED
        ========================================================= */
        [HttpGet("{id}/images")]
        public IActionResult GetRequestImages(string id)
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var request = _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefault(r =>
                    r.RequestId == id && // 👈 Query by RequestId
                    r.Donation != null &&
                    r.Donation.AssignedHubId == hubId &&
                    r.Donation.District == district // ✅ DISTRICT SECURITY CHECK
                );

            if (request == null || request.Donation == null)
                return NotFound("Request not found for this hub");

            var images = _context.DonationImages
                .Where(i => i.DonationId == request.Donation.Id)
                .OrderBy(i => i.Id)
                .Select(i => i.ImageUrl)
                .ToList();

            return Ok(images);
        }
        // 🔹 GET: Request history (Rejected, Collected)
        [HttpGet("history")]
        public IActionResult GetRequestHistory()
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var history = _context.DonationRequests
                .Include(r => r.Donation)
                .Include(r => r.Requester)
                .Where(r =>
                    r.Donation != null &&
                    r.Donation.AssignedHubId == hubId &&
                    r.Donation.District == district &&
                    (r.Status == "Rejected" || r.Status == "Collected")
                )
                .OrderByDescending(r =>
                    r.DeliveredAt ?? r.RejectedAt
                )
                .Select(r => new HubRequestDto
                {
                    RequestId = !string.IsNullOrEmpty(r.RequestId) ? r.RequestId : $"REQ-{r.Id}", // 👈 Handle legacy data
                    DonationId = r.DonationId,
                    DonationCode = r.Donation!.DonationId,
                    ItemTitle = r.Donation.Title,
                    Category = r.Donation.Category,
                    RequesterName = r.Requester != null ? r.Requester.FullName : "Unknown",
                    District = r.Donation.District,
                    Status = r.Status,
                    RequestedAt = r.RequestedAt,
                    RejectionReason = r.RejectionReason,
                    ApprovedAt = r.ApprovedAt,
                    RejectedAt = r.RejectedAt,
                    DeliveredAt = r.DeliveredAt,
                    ThumbnailImageUrl = r.Donation.ThumbnailImageUrl
                })
                .ToList();

            return Ok(history);
        }
        // 🔹 POST: Mark request as collected
        [HttpPost("{id}/collect")]
        public IActionResult MarkRequestAsCollected(string id)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var request = _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefault(r =>
                    r.RequestId == id && // 👈 Query by RequestId
                    r.Donation != null &&
                    r.Donation.AssignedHubId == hubId &&
                    r.Donation.District == district
                );

            if (request == null)
                return NotFound("Request not found");

            if (request.Status != "Approved")
                return BadRequest("Only approved requests can be collected");

            request.Status = "Collected";
            request.DeliveredAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Request marked as collected" });
        }

    }
}