using Clothsy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize(Roles = "HUB")]
[ApiController]
[Route("api/web/hub/dashboard")]
public class HubDashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HubDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetDashboard()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var webUser = _context.WebUsers
            .FirstOrDefault(x => x.Email == email && x.Role == "HUB");

        if (webUser == null)
            return Unauthorized();

        // ✅ Hub IS the WebUser
        var hubId = webUser.Id;

        var response = new
        {
            PendingApprovals = _context.Donations
                .Count(d => d.AssignedHubId == hubId && d.Status == "Waiting"),

            ApprovedItems = _context.Donations
                .Count(d => d.AssignedHubId == hubId && d.Status == "Approved"),

            ActiveRequests = _context.Requests
                .Count(r => r.HubId == hubId && r.Status == "Active"),

            TotalDistributed = _context.Requests
                .Count(r => r.HubId == hubId && r.Status == "Completed")
        };

        return Ok(response);
    }
}
