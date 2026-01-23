using Clothsy.Data;
using Clothsy.DTOs.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Clothsy.Controllers.Admin
{
    [Route("api/admin/settings")]
    [ApiController]
    public class AdminSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Get system settings
        [HttpGet]
        public IActionResult GetSettings()
        {
            var settings = _context.SystemSettings.FirstOrDefault();

            if (settings == null)
                return NotFound("Settings not initialized");

            return Ok(settings);
        }

        // 🔹 Update system settings
        [HttpPost]
        public IActionResult UpdateSettings([FromBody] UpdateSystemSettingsDto dto)
        {
            var settings = _context.SystemSettings.FirstOrDefault();

            if (settings == null)
                return NotFound("Settings not initialized");

            settings.SystemName = dto.SystemName;
            settings.AdminEmail = dto.AdminEmail;
            settings.SupportEmail = dto.SupportEmail;
            settings.AutoArchiveDays = dto.AutoArchiveDays;
            settings.EmailNotificationsEnabled = dto.EmailNotificationsEnabled;
            settings.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "System settings updated" });
        }
    }
}
