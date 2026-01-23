using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;
using Clothsy.DTOs.Admin;
using Clothsy.Models.Donation;

namespace Clothsy.Controllers.Admin
{
    [Route("api/admin/hubs")]
    [ApiController]
    public class AdminHubsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminHubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 List hubs
        [HttpGet]
        public IActionResult GetHubs()
        {
            var hubs = _context.Hubs
                .Select(h => new
                {
                    h.Id,
                    h.HubCode,
                    h.Name,
                    h.District,
                    Status = h.IsActive ? "Active" : "Inactive",
                    Inventory = _context.Donations.Count(d =>
                        d.AssignedHubId == h.Id && d.Status == "Approved")
                })
                .ToList();

            return Ok(hubs);
        }

        // 🔹 Add hub
        [HttpPost]
        public IActionResult AddHub([FromBody] AddHubDto dto)
        {
            var hub = new Hub
            {
                HubCode = $"HUB{DateTime.UtcNow.Ticks.ToString()[^4..]}",
                Name = dto.HubName,
                Address = dto.Address,
                District = dto.District,
                Email = dto.HubEmail,
                Phone = dto.HubPhone,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.Hubs.Add(hub);
            _context.SaveChanges();

            return Ok(new { message = "Hub created successfully" });
        }

        // 🔹 Deactivate hub
        [HttpPost("{id}/deactivate")]
        public IActionResult DeactivateHub(int id)
        {
            var hub = _context.Hubs.FirstOrDefault(h => h.Id == id);
            if (hub == null) return NotFound();

            hub.IsActive = false;
            _context.SaveChanges();

            return Ok();
        }

        // 🔹 Activate hub
        [HttpPost("{id}/activate")]
        public IActionResult ActivateHub(int id)
        {
            var hub = _context.Hubs.FirstOrDefault(h => h.Id == id);
            if (hub == null) return NotFound();

            hub.IsActive = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
