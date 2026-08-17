using Microsoft.AspNetCore.Identity;
using Shopera.Application.DTOs.User;
using Shopera.Application.IService.User;
using Shopera.Domain.Entities;

namespace Shopera.Application.Service.User
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Password and ConfirmPassword do not match."
                    });
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                CreateDate = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }
    }
}
