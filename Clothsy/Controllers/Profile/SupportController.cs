
using Clothsy.Data;
using Clothsy.DTOs.Profile;
using Clothsy.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateSupport([FromBody] CreateSupportRequest request)
        {
            Console.WriteLine("========== SUPPORT DEBUG ==========");
            Console.WriteLine("AUTH HEADER: " + Request.Headers["Authorization"]);

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"CLAIM: {claim.Type} = {claim.Value}");
            }
            Console.WriteLine("===================================");


            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            string? imageUrl = null;

            // Optional image handling
            if (!string.IsNullOrEmpty(request.ImageBase64))
            {
                var bytes = Convert.FromBase64String(request.ImageBase64);
                var fileName = $"{Guid.NewGuid()}.jpg";
                var path = Path.Combine("wwwroot/support", fileName);

                Directory.CreateDirectory("wwwroot/support");
                await System.IO.File.WriteAllBytesAsync(path, bytes);

                imageUrl = $"/support/{fileName}";
            }

            var ticket = new SupportTicket
            {
                UserId = userId,
                Category = request.Category,
                Message = request.Message,
                ImageUrl = imageUrl,
                Status = "Open",
                CreatedAt = DateTime.Now
            };

            _context.SupportTickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Issue reported successfully"
            });
        }
    }
}
