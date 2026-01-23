using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothsy.Controllers.Web.Hub
{
    /// <summary>
    /// Base controller for all Hub endpoints with reusable helper methods
    /// </summary>
    [Authorize(Roles = "HUB")]
    [ApiController]
    public abstract class BaseHubController : ControllerBase
    {
        /// <summary>
        /// Extracts HubId from JWT token claims
        /// </summary>
        /// <returns>Hub ID</returns>
        /// <exception cref="UnauthorizedAccessException">If HubId claim is missing</exception>
        protected int GetHubId()
        {
            var claim = User.FindFirst("HubId");
            if (claim == null)
                throw new UnauthorizedAccessException("HubId missing in token");

            return int.Parse(claim.Value);
        }

        /// <summary>
        /// Extracts District from JWT token claims
        /// </summary>
        /// <returns>District name</returns>
        /// <exception cref="UnauthorizedAccessException">If District claim is missing</exception>
        protected string GetDistrict()
        {
            var claim = User.FindFirst("District");
            if (claim == null)
                throw new UnauthorizedAccessException("District missing in token");

            return claim.Value;
        }
    }
}