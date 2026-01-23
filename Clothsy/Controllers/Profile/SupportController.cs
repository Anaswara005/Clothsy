using Clothsy.Data;
using Clothsy.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clothsy.Controllers.Profile
{
    [Authorize]
    [ApiController]
    [Route("api/support")]
    public class SupportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SupportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSupport(
            [FromForm] string Category,
            [FromForm] string Message,
            [FromForm] int DistrictId,
            IFormFile? image)
        {
            // 🔐 Get user ID
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { success = false, message = "Unauthorized" });

            int userId = int.Parse(userIdClaim.Value);

            // ✅ Validate district
            var hub = await _context.Hubs
                .FirstOrDefaultAsync(h => h.DistrictId == DistrictId && h.IsActive);

            if (hub == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid district selected"
                });
            }

            // 📷 Handle optional image upload
            string? imageUrl = null;
            if (image != null && image.Length > 0)
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var folder = Path.Combine("wwwroot", "support");
                    Directory.CreateDirectory(folder);

                    var path = Path.Combine(folder, fileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    await image.CopyToAsync(stream);

                    imageUrl = $"/support/{fileName}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error uploading image: {ex.Message}");
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Failed to upload image: {ex.Message}"
                    });
                }
            }

            // 🎫 Create support ticket
            var ticket = new SupportTicket
            {
                UserId = userId,
                Category = Category,
                Message = Message,
                DistrictId = DistrictId,
                District = hub.District,
                ImageUrl = imageUrl,
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };

            _context.SupportTickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                ticketId = ticket.Id,
                message = "Issue reported successfully"
            });
        }

        [HttpGet("my")]
        public IActionResult GetMyTickets()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var tickets = _context.SupportTickets
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new
                {
                    t.Id,
                    t.Category,
                    t.District,
                    t.Status,
                    t.CreatedAt
                })
                .ToList();

            return Ok(new
            {
                success = true,
                data = tickets
            });
        }

        [HttpGet("my/{ticketId}")]
        public IActionResult GetMyTicketDetails(int ticketId)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var ticket = _context.SupportTickets
                .Where(t => t.Id == ticketId && t.UserId == userId)
                .Select(t => new
                {
                    t.Id,
                    t.Category,
                    t.Message,
                    t.ImageUrl,
                    t.District,
                    t.Status,
                    t.CreatedAt,
                    Replies = t.Replies
                        .OrderBy(r => r.CreatedAt)
                        .Select(r => new
                        {
                            r.Message,
                            r.SenderRole,
                            r.ImageUrl,
                            r.CreatedAt
                        })
                })
                .FirstOrDefault();

            if (ticket == null)
                return NotFound(new { success = false, message = "Ticket not found" });

            return Ok(new
            {
                success = true,
                data = ticket
            });
        }

        [Authorize(Roles = "Hub,Admin")]
        [HttpPost("{ticketId}/reply")]
        public async Task<IActionResult> ReplyToTicket(
            int ticketId,
            [FromForm] string Message,
            [FromForm] string? NewStatus = null)
        {
            var ticket = await _context.SupportTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                return NotFound(new { success = false, message = "Ticket not found" });

            var reply = new SupportTicketReply
            {
                TicketId = ticketId,
                Message = Message,
                SenderRole = User.IsInRole("Admin") ? "ADMIN" : "HUB",
                CreatedAt = DateTime.UtcNow
            };

            _context.SupportTicketReplies.Add(reply);

            // Optional status change
            if (!string.IsNullOrEmpty(NewStatus))
                ticket.Status = NewStatus;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Reply added successfully" });
        }
    }
}