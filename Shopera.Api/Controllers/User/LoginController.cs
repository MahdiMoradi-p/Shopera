using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;

namespace Shopera.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : UserBaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _loginService.LoginAsync(model);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            return Ok(new
            {
                Message = "Login successful",
                UserName = user.UserName
            });
        }
    }
}