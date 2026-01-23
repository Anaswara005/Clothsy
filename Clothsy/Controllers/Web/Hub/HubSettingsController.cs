using Clothsy.Data;
using Clothsy.DTOs.Web.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clothsy.Controllers.Web.Hub
{
    [Authorize(Roles = "HUB")]
    [ApiController]
    [Route("api/web/hub/settings")]
    public class HubSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HubSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Helper
        private int GetHubId()
        {
            return int.Parse(User.FindFirst("HubId")!.Value);
        }

        // =========================================================
        // 1. GET HUB INFO (READ-ONLY FIELDS INCLUDED)
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetHubSettings()
        {
            var hubId = GetHubId();

            var hub = await _context.Hubs
                .Where(h => h.Id == hubId)
                .Select(h => new
                {
                    hubName = h.Name,
                    district = h.District,
                    email = h.Email,
                    address = h.Address,
                    phone = h.Phone,
                    openingTime = h.OpenTime,
                    closingTime = h.CloseTime,
                    workingDays = h.WorkingDays
                })
                .FirstOrDefaultAsync();

            return Ok(hub);
        }


        // =========================================================
        // 2. UPDATE HUB CONTACT INFO
        // =========================================================
        [HttpPut("contact")]
        public async Task<IActionResult> UpdateContactInfo(
            [FromBody] HubSettingsDto dto)
        {
            var hubId = GetHubId();
            var hub = await _context.Hubs.FindAsync(hubId);

            if (hub == null)
                return NotFound();

            hub.Address = dto.Address;
            hub.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Contact details updated" });
        }

        // =========================================================
        // 3. UPDATE OPERATING HOURS
        // =========================================================
        [HttpPut("hours")]
        public async Task<IActionResult> UpdateOperatingHours(
     [FromBody] HubOperatingHoursDto dto)
        {
            var hub = await _context.Hubs.FindAsync(GetHubId());
            if (hub == null) return NotFound();

            hub.OpenTime = TimeSpan.Parse(dto.OpeningTime);
            hub.CloseTime = TimeSpan.Parse(dto.ClosingTime);
            hub.WorkingDays = dto.WorkingDays;

            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
