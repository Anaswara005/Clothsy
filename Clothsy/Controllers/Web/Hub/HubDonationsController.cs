using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub/donations")]
    public class HubDonationsController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: Pending donations for logged-in hub (DISTRICT-FILTERED)
        [HttpGet("pending")]
        public IActionResult GetPendingDonations()
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var pendingDonations = _context.Donations
                .Include(d => d.Donor)
                .Where(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district &&
                    (d.Status == "Waiting" ||
 d.Status == "Pending" ||
 d.Status == "Received")
                // ✅ Accept BOTH statuses
                )
                .Select(d => new PendingDonationDto
                {
                    Id = d.Id,
                    DonationId = d.DonationId,
                    Title = d.Title,
                    Description = d.Description,
                    Category = d.Category,
                    ThumbnailImageUrl = d.ThumbnailImageUrl,
                    SubmittedAt = d.CreatedAt,
                    DonorName = d.Donor != null ? d.Donor.FullName : "Unknown",
                    Status = d.Status
                })
                .OrderByDescending(d => d.SubmittedAt)
                .ToList();

            return Ok(pendingDonations);
        }

        // 🔹 POST: Approve donation (DISTRICT-SECURED)
        [HttpPost("{id}/approve")]
        public IActionResult ApproveDonation(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var donation = _context.Donations
                .FirstOrDefault(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district
                );

            if (donation == null)
                return NotFound("Donation not found for this hub");



            // ✅ CORRECT: approve ONLY after item is received
            if (donation.Status != "Received")
                return BadRequest("Item must be received before approval");




            donation.Status = "Approved";
            donation.ApprovedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Donation approved" });
        }

        // 🔹 GET: All images for a donation (DISTRICT-SECURED)
        [HttpGet("{id}/images")]
        public IActionResult GetDonationImages(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var belongsToHub = _context.Donations
                .Any(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district
                );

            if (!belongsToHub)
                return Unauthorized("Donation does not belong to this hub");

            var images = _context.DonationImages
                .Where(i => i.DonationId == id)
                .OrderBy(i => i.Id)
                .Select(i => i.ImageUrl)
                .ToList();

            return Ok(images);
        }

        // 🔹 POST: Reject donation
        [HttpPost("{id}/reject")]
        public IActionResult RejectDonation(int id, [FromBody] RejectDonationDto dto)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var donation = _context.Donations
                .FirstOrDefault(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district
                );

            if (donation == null)
                return NotFound("Donation not found");

            // ✅ Accept both Waiting and Pending
            if (donation.Status != "Received")
                return BadRequest("Item must be received before approval");


            donation.Status = "Rejected";
            donation.RejectedAt = DateTime.UtcNow;
            donation.RejectionReason = dto.Reason;

            _context.SaveChanges();

            return Ok(new { message = "Donation rejected" });
        }
        // 🔹 GET: Donation history (Approved, Rejected, Collected)
        [HttpGet("history")]
        public IActionResult GetDonationHistory()
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var history = _context.Donations
                .Include(d => d.Donor)
                .Where(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district &&
                    (d.Status == "Approved" ||
                     d.Status == "Rejected" ||
                     d.Status == "Collected")
                )
                .Select(d => new DonationHistoryDto
                {
                    Id = d.Id,
                    DonationId = d.DonationId,
                    Title = d.Title,
                    Category = d.Category,
                    ThumbnailImageUrl = d.ThumbnailImageUrl,
                    DonorName = d.Donor != null ? d.Donor.FullName : "Unknown",
                    Status = d.Status,
                    ApprovedAt = d.ApprovedAt,
                    RejectedAt = d.RejectedAt,
                    CollectedAt = d.CollectedAt
                })
                .OrderByDescending(d =>
                    d.CollectedAt ?? d.RejectedAt ?? d.ApprovedAt
                )
                .ToList();

            return Ok(history);
        }
        // 🔹 POST: Mark donation as collected
        [HttpPost("{id}/collect")]
        public IActionResult MarkAsCollected(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var donation = _context.Donations
                .FirstOrDefault(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district
                );

            if (donation == null)
                return NotFound("Donation not found");

            if (donation.Status != "Approved")
                return BadRequest("Only approved donations can be collected");

            donation.Status = "Collected";
            donation.CollectedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Donation marked as collected" });
        }
        // 🔹 POST: Mark donation as physically received at hub
        [HttpPost("{id}/received")]
        public IActionResult MarkAsReceived(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict();

            var donation = _context.Donations
                .FirstOrDefault(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district
                );

            if (donation == null)
                return NotFound("Donation not found");

            // ✅ CORRECT: can mark received ONLY from Waiting / Pending
            if (donation.Status != "Waiting" && donation.Status != "Pending")
                return BadRequest("Donation already processed");



            donation.Status = "Received";
            donation.ReceivedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Donation marked as received" });
        }

    }
}