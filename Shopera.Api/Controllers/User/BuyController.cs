using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopera.Application.IService.Buy;
using System.Security.Claims;

namespace Shopera.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class BuyController : ControllerBase
    {
        private readonly IBuyService _buyService;

        public BuyController(IBuyService buyService)
        {
            _buyService = buyService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var result =
                    await _buyService.CheckoutAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}