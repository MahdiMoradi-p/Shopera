using Microsoft.AspNetCore.Identity;
using Shopera.Application.DTOs.User;

namespace Shopera.Application.IService.User
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model);
    }
}