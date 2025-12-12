using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Shopera.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminBaseController : ControllerBase
    {

        private string? _cachedUserId;
        protected string? UserId
        {
            get
            {
                if (_cachedUserId == null)
                {
                    _cachedUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return _cachedUserId;
            }
        }
    }
}