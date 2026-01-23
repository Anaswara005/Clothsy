using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Clothsy.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub/support-tickets")]
    public class HubSupportTicketsController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubSupportTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        /* =========================================================
           GET : Tickets for this hub (DISTRICT BASED)
        ========================================================= */
        [HttpGet]
        public IActionResult GetTickets()
        {
            var district = GetDistrict();

            var tickets = _context.SupportTickets
                .Include(t => t.User)
                .Where(t => t.District == district)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new SupportTicketDto
                {
                    TicketId = t.Id,
                    UserName = t.User.FullName,
                    Category = t.Category,
                    Message = t.Message,
                    District = t.District,
                    ImageUrl = t.ImageUrl,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt
                })
                .ToList();

            return Ok(tickets);
        }

        /* =========================================================
           POST : Resolve ticket
        ========================================================= */
        [HttpPost("{id}/resolve")]
        public IActionResult ResolveTicket(int id)
        {
            var district = GetDistrict();

            var ticket = _context.SupportTickets
                .FirstOrDefault(t => t.Id == id && t.District == district);

            if (ticket == null)
                return NotFound("Ticket not found for this hub");

            ticket.Status = "Resolved";
            _context.SaveChanges();

            return Ok(new { message = "Ticket resolved" });
        }

        /* =========================================================
           POST : Reject ticket
        ========================================================= */
        [HttpPost("{id}/reject")]
        public IActionResult RejectTicket(int id)
        {
            var district = GetDistrict();

            var ticket = _context.SupportTickets
                .FirstOrDefault(t => t.Id == id && t.District == district);

            if (ticket == null)
                return NotFound("Ticket not found for this hub");

            ticket.Status = "Rejected";
            _context.SaveChanges();

            return Ok(new { message = "Ticket rejected" });
        }

        /* =========================================================
           POST : Reply to ticket
        ========================================================= */
        [HttpPost("{id}/reply")]
        public async Task<IActionResult> ReplyToTicket(int id, [FromForm] string message, [FromForm] IFormFile? image)
        {
            var district = GetDistrict();

            var ticket = _context.SupportTickets
                .FirstOrDefault(t => t.Id == id && t.District == district);

            if (ticket == null)
                return NotFound("Ticket not found for this hub");

            string? imageUrl = null;

            // Handle image upload
            if (image != null && image.Length > 0)
            {
                Directory.CreateDirectory("wwwroot/uploads/support");
                var fileName = $"{Guid.NewGuid()}.jpg";
                var path = Path.Combine("wwwroot/uploads/support", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrl = $"http://10.0.2.2:7269/uploads/support/{fileName}";
            }

            ticket.Status = "Resolved";

            var reply = new SupportTicketReply
            {
                TicketId = ticket.Id,
                SenderRole = "HUB",
                Message = message,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.SupportTicketReplies.Add(reply);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reply sent & ticket resolved" });
        }

        /* =========================================================
           GET : Ticket with replies
        ========================================================= */
        [HttpGet("{id}")]
        public IActionResult GetTicketWithReplies(int id)
        {

            var district = GetDistrict();

            var ticket = _context.SupportTickets
                .Include(t => t.User)
                .Include(t => t.Replies)
                .FirstOrDefault(t => t.Id == id && t.District == district);

            if (ticket == null)
                return NotFound();

            var dto = new SupportTicketDetailDto
            {
                TicketId = ticket.Id,
                UserName = ticket.User.FullName,
                Category = ticket.Category,
                Message = ticket.Message,
                District = ticket.District,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
                ImageUrl = ticket.ImageUrl,
                Replies = ticket.Replies
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => new SupportTicketReplyDto
                    {
                        Id = r.Id,
                        TicketId = r.TicketId,
                        SenderRole = r.SenderRole,
                        Message = r.Message,
                        ImageUrl = r.ImageUrl,
                        CreatedAt = r.CreatedAt
                    })
                    .ToList()
            };

            return Ok(dto);
        }
    }
}
