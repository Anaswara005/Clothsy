using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clothsy.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public abstract class BaseAdminController : ControllerBase
    {
        protected int GetAdminId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        protected string GetAdminEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
