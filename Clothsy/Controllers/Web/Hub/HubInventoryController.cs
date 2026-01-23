using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub/inventory")]
    public class HubInventoryController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: Inventory items (approved + available) - DISTRICT FILTERED
        [HttpGet]
        public IActionResult GetInventory()
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var items = _context.Donations
                .Include(d => d.Donor)
                .Where(d =>
                    d.AssignedHubId == hubId &&
                    d.District == district && // ✅ DISTRICT FILTER
                    d.Status == "Approved" &&
                    d.IsAvailable
                )
                .OrderByDescending(d => d.ApprovedAt)
                .Select(d => new InventoryItemDto
                {
                    Id = d.Id,
                    DonationId = d.DonationId,
                    Title = d.Title,
                    Category = d.Category,
                    ClothingType = d.ClothingType,
                    Size = d.Size,
                    Description = d.Description,
                    ThumbnailImageUrl = d.ThumbnailImageUrl,
                    DonorName = d.Donor != null ? d.Donor.FullName : "Unknown",
                    District = d.District,
                    ApprovedAt = d.ApprovedAt ?? d.CreatedAt
                })
                .ToList();

            return Ok(items);
        }

        // 🔹 POST: Mark item unavailable (after distribution) - DISTRICT SECURED
        [HttpPost("{id}/mark-unavailable")]
        public IActionResult MarkUnavailable(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var donation = _context.Donations
                .FirstOrDefault(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ DISTRICT SECURITY CHECK
                );

            if (donation == null)
                return NotFound("Item not found for this hub");

            if (!donation.IsAvailable)
                return BadRequest("Item already unavailable");

            donation.IsAvailable = false;
            _context.SaveChanges();

            return Ok(new { message = "Item marked unavailable" });
        }

        // 🔹 GET: Inventory item images (secured) - DISTRICT SECURED
        [HttpGet("{id}/images")]
        public IActionResult GetInventoryItemImages(int id)
        {
            var hubId = GetHubId();
            var district = GetDistrict(); // ✅ GET DISTRICT

            var belongsToHub = _context.Donations
                .Any(d =>
                    d.Id == id &&
                    d.AssignedHubId == hubId &&
                    d.District == district // ✅ DISTRICT SECURITY CHECK
                );

            if (!belongsToHub)
                return Unauthorized("Item does not belong to this hub");

            var images = _context.DonationImages
                .Where(i => i.DonationId == id)
                .OrderBy(i => i.Id)
                .Select(i => i.ImageUrl)
                .ToList();

            return Ok(images);
        }
    }
}