using Shopera.Application.DTOs.User;
using Shopera.Domain.Entities;

namespace Shopera.Application.IService.User
{
    public interface ILoginService
    {
        Task<ApplicationUser?> LoginAsync(LoginDto model);
    }
}