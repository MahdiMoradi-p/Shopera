using Microsoft.AspNetCore.Identity;
using Shopera.Application.IService;

namespace Shopera.Application.Service.User
{
    private readonly UserManager<ApplicationUser> userManager;

    class RegisterService(userManager) : IRegisterService
    {

    }
}
