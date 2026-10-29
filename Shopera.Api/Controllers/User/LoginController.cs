using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;

namespace Shopera.Api.Controllers.User
{
    [ApiController]
    public class LoginController : UserBaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _loginService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Username or password is incorrect.");

            return Ok(result);
        }
    }
}