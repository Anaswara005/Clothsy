using Clothsy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class HomepageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomepageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/homepage/approved-items
        [HttpGet("approved-items")]
        public async Task<IActionResult> GetApprovedItems(
            [FromQuery] string? category = null,
            [FromQuery] string? search = null)
        {
            var query = _context.Donations
                .Include(d => d.Images)
                .Include(d => d.Donor)
               .Where(d => d.Status == "Approved" && d.IsAvailable == true);

            // Filter by category if provided
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                query = query.Where(d => d.Category == category);
            }

            // Search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.Title.Contains(search) ||
                    d.Category.Contains(search) ||
                    d.Size.Contains(search) ||
                    d.Description!.Contains(search));
            }

            var items = await query
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.DonationId,
                    d.Title,
                    d.Category,
                    d.Size,
                    d.ThumbnailImageUrl,
                    d.Description,
                    d.Status,
                    d.CreatedAt,
                    donorName = d.Donor!.FullName,
                    donorId = d.DonorUserId,
                    imageCount = d.Images.Count
                })
                .ToListAsync();

            return Ok(new { success = true, data = items });
        }

        // GET: api/homepage/item-details/{id}
        [HttpGet("item-details/{id}")]
        public async Task<IActionResult> GetItemDetails(int id)
        {
            var item = await _context.Donations
                .Include(d => d.Images)
                .Include(d => d.Donor)
                .Include(d => d.AssignedHub).Where(d => d.Id == id && d.Status == "Approved" && d.IsAvailable == true)
                .Select(d => new
                {
                    d.Id,
                    d.DonationId,
                    d.Title,
                    d.Category,
                    d.ClothingType,
                    d.Size,
                    d.Description,
                    d.Status,
                    d.District,
                    d.CreatedAt,
                    donor = new
                    {
                        d.Donor!.Id,
                        d.Donor.FullName,
                        d.Donor.Email
                    },
                    hub = new
                    {
                        d.AssignedHub!.Name,
                        d.AssignedHub.Address,
                        d.AssignedHub.District
                    },
                    images = d.Images.Select(img => new
                    {
                        img.Id,
                        img.ImageUrl,
                        img.IsPrimary
                    }).OrderByDescending(img => img.IsPrimary).ToList()
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return NotFound(new { success = false, message = "Item not found or not approved" });

            return Ok(new { success = true, data = item });
        }

        // GET: api/homepage/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = new List<string>
            {
                "All", "Men", "Women", "Kids", "Unisex",
                "Tops", "Bottoms", "Jackets", "Dresses",
                "Winter", "Footwear"
            };
            await Task.CompletedTask;
            return Ok(new { success = true, data = categories });
        }

        // GET: api/homepage/notifications
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            // For now, return dummy notifications
            // Later implement proper notification system
            var notifications = new List<object>
            {
                new
                {
                    id = 1,
                    type = "donation_approved",
                    message = "Your donation 'Blue T-Shirt' has been approved!",
                    createdAt = DateTime.UtcNow.AddHours(-2),
                    isRead = false
                }
            };
            await Task.CompletedTask;
            return Ok(new { success = true, data = notifications });
        }
    }
}