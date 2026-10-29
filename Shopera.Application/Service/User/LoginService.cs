using Microsoft.AspNetCore.Identity;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;
using Shopera.Domain.Entities;

namespace Shopera.Application.Service.User
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> LoginAsync(
            LoginDto model)
        {
            var user =
                await _userManager.FindByNameAsync(
                    model.UserName);

            if (user == null)
                return null;

            var isPasswordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password);

            if (!isPasswordValid)
                return null;

            var token =
                await _jwtService.GenerateTokenAsync(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            };
        }

       
    }
}