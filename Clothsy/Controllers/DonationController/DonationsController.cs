using Clothsy.Data;
using Clothsy.DTOs;
using Clothsy.DTOs.DonationDTOs;
using Clothsy.Models;
using Clothsy.Models.Donation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace Clothsy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DonationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/donations - Create new donation
        [HttpPost]
        public async Task<IActionResult> CreateDonation([FromBody] CreateDonationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Find hub by district
            var hub = await _context.Hubs
                .FirstOrDefaultAsync(h => h.District == request.District && h.IsActive);

            if (hub == null)
                return BadRequest(new { success = false, message = "No hub available for this district" });

            // Generate unique Donation ID (CLO-1763630985695 format)
            var donationId = GenerateDonationId();

            var donation = new Donation
            {
                DonationId = donationId,
                DonorUserId = userId,
                Category = request.Category,
                ClothingType = request.ClothingType,
                Size = request.Size,
                Description = request.Description,
                District = request.District,
                AssignedHubId = hub.Id,
                Photo1Url = request.Photo1Url,
                Photo2Url = request.Photo2Url,
                Photo3Url = request.Photo3Url,
                Status = "Waiting",
                CreatedAt = DateTime.UtcNow
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Donation Created Successfully!",
                data = new
                {
                    donationId = donation.DonationId,
                    categoryType = $"{donation.Category} - {donation.ClothingType}",
                    size = donation.Size,
                    district = donation.District,
                    createdOn = donation.CreatedAt.ToString("dd-MM-yyyy"),
                    hubInfo = new
                    {
                        name = hub.Name,
                        address = hub.Address,
                        email = hub.Email,
                        phone = hub.Phone
                    }
                }
            });
        }

        // GET: api/donations/mydonations - Get my donations list
        [HttpGet("mydonations")]
        public async Task<IActionResult> GetMyDonations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donations = await _context.Donations
                .Include(d => d.AssignedHub)
                .Where(d => d.DonorUserId == userId)
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.DonationId,
                    title = $"{d.Category} - {d.ClothingType}",
                    d.Size,
                    d.District,
                    d.Status, // Waiting, Approved, Rejected
                    d.CreatedAt
                })
                .ToListAsync();

            return Ok(new { success = true, data = donations });
        }

        // GET: api/donations/{id} - Get donation details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonationDetails(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donation = await _context.Donations
                .Include(d => d.AssignedHub)
                .FirstOrDefaultAsync(d => d.Id == id && d.DonorUserId == userId);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

            return Ok(new
            {
                success = true,
                data = new
                {
                    donation.Id,
                    donation.DonationId,
                    donation.Category,
                    donation.ClothingType,
                    donation.Size,
                    donation.District,
                    donation.Description,
                    donation.Status,
                    donation.Photo1Url,
                    donation.Photo2Url,
                    donation.Photo3Url,
                    createdOn = donation.CreatedAt.ToString("dd-MM-yyyy"),
                    hubInfo = new
                    {
                        donation.AssignedHub!.Name,
                        donation.AssignedHub.Address,
                        donation.AssignedHub.Email,
                        donation.AssignedHub.Phone
                    }
                }
            });
        }

        // DELETE: api/donations/{id} - Delete donation (only if Waiting status)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.Id == id && d.DonorUserId == userId);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

            if (donation.Status != "Waiting")
                return BadRequest(new { success = false, message = "Cannot delete donation that is already processed" });

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Donation deleted successfully" });
        }

        // PUT: api/donations/{id}/status - Update donation status (Admin only - for demo)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateDonationStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            var donation = await _context.Donations.FindAsync(id);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

            donation.Status = request.Status; // Waiting, Approved, Rejected

            if (request.Status == "Approved")
                donation.ApprovedAt = DateTime.UtcNow;
            else if (request.Status == "Rejected")
                donation.RejectedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Status updated successfully" });
        }

        // Helper method to generate unique donation ID
        private string GenerateDonationId()
        {
            // Generate format: CLO-1763630985695
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return $"CLO-{timestamp}";
        }
    }

    // DTO for status update
    public class StatusUpdateRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty; // Waiting, Approved, Rejected
    }
}