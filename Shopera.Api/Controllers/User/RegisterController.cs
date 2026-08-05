using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;

namespace Shopera.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : UserBaseController
    {
        private readonly IRegisterService _registerService;

        public AccountController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _registerService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Registration successful");
        }
    }
}