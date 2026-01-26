using Clothsy.Data;
using Clothsy.Models.Profile;
using Clothsy.Models.SignupModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clothsy.Controllers.Web.Admin
{
    [Route("api/admin/complaints")]
    [ApiController]
    public class AdminComplaintsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/admin/complaints
        [HttpGet]
        public async Task<IActionResult> GetAllComplaints()
        {
            var complaints = await _context.SupportTickets
                .Include(t => t.User) // Include User details if needed
                .Include(t => t.Replies)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new
                {
                    t.Id,
                    Subject = t.Category, // Mapped to Category as requested
                    // Model has: Category, Message, DistrictId, District, ImageUrl, Status, CreatedAt
                    t.Category,
                    t.Message,
                    t.District,
                    t.DistrictId,
                    t.Status,
                    t.CreatedAt,
                    t.ImageUrl,
                    User = new
                    {
                        Id = t.User.Id,
                        t.User.FullName,
                        t.User.Email,
                        t.User.PhoneNumber
                    },
                    ReplyCount = t.Replies.Count,
                    Replies = t.Replies.OrderBy(r => r.CreatedAt).Select(r => new
                    {
                        r.Id,
                        r.Message,
                        r.CreatedAt,
                        r.SenderRole
                    }).ToList()
                })
                .ToListAsync();

            return Ok(complaints);
        }

        // PUT: api/admin/complaints/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var ticket = await _context.SupportTickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound(new { message = "Complaint not found" });
            }

            // Validate status
            var allowedStatuses = new[] { "Open", "InProgress", "Resolved", "Rejected" };
            if (!allowedStatuses.Contains(request.Status))
            {
                return BadRequest(new { message = "Invalid status. Allowed: Open, InProgress, Resolved, Rejected" });
            }

            ticket.Status = request.Status;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Complaint status updated to {request.Status}", ticket });
        }

        // Helper DTO class
        public class UpdateStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }
    }
}
