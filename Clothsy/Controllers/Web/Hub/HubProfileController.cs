using Clothsy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothsy.Controllers.Web.Hub
{
    [Route("api/web/hub")]
    public class HubProfileController : BaseHubController
    {
        private readonly ApplicationDbContext _context;

        public HubProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        /* =========================================================
           GET : Hub Profile (Name + District)
        ========================================================= */
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var hubId = GetHubId(); // From BaseHubController

            var hub = _context.Hubs
                .FirstOrDefault(h => h.Id == hubId);

            if (hub == null)
                return NotFound("Hub not found");

            return Ok(new
            {
                hubId = hub.Id,
                hubName = hub.Name,
                district = hub.District,
                email = hub.Email
            });
        }
    }
}