using Microsoft.AspNetCore.Identity;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;
using Shopera.Domain.Entities;

namespace Shopera.Application.Service.User
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return null;

            var isPasswordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password);
            return isPasswordValid ? user : null;
        }
    }
}