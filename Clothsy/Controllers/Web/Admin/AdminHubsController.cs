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
                    h.Address,
                    h.Email,
                    h.Phone,
                    h.Pincode,
                    Status = h.IsActive ? "Active" : "Inactive",
                    // Available Items = Only items with "Approved" status
                    Inventory = _context.Donations.Count(d =>
    d.AssignedHubId == h.Id &&
    d.Status == "Approved" &&
    d.IsAvailable)  // ← Add this check
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
                Pincode = dto.Pincode,
                Email = dto.HubEmail,
                Phone = dto.HubPhone,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
            _context.Hubs.Add(hub);
            _context.SaveChanges();
            return Ok(new { message = "Hub created successfully" });
        }

        // 🔹 Update hub
        [HttpPut("{id}")]
        public IActionResult UpdateHub(int id, [FromBody] AddHubDto dto)
        {
            try
            {
                var hub = _context.Hubs.FirstOrDefault(h => h.Id == id);
                if (hub == null) return NotFound(new { message = "Hub not found" });

                hub.Name = dto.HubName;
                hub.Address = dto.Address;
                hub.District = dto.District;
                hub.Pincode = dto.Pincode;
                hub.Email = dto.HubEmail;
                hub.Phone = dto.HubPhone;
                hub.IsActive = dto.IsActive;

                _context.SaveChanges();
                return Ok(new { message = "Hub updated successfully", hubId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating hub", error = ex.Message });
            }
        }

        // 🔹 Deactivate hub
        [HttpPost("{id}/deactivate")]
        public IActionResult DeactivateHub(int id)
        {
            try
            {
                var hub = _context.Hubs.FirstOrDefault(h => h.Id == id);
                if (hub == null) return NotFound(new { message = "Hub not found" });
                
                hub.IsActive = false;
                _context.SaveChanges();
                
                return Ok(new { message = "Hub deactivated successfully", hubId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deactivating hub", error = ex.Message });
            }
        }

        // 🔹 Activate hub
        [HttpPost("{id}/activate")]
        public IActionResult ActivateHub(int id)
        {
            try
            {
                var hub = _context.Hubs.FirstOrDefault(h => h.Id == id);
                if (hub == null) return NotFound(new { message = "Hub not found" });
                
                hub.IsActive = true;
                _context.SaveChanges();
                
                return Ok(new { message = "Hub activated successfully", hubId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error activating hub", error = ex.Message });
            }
        }
    }
}