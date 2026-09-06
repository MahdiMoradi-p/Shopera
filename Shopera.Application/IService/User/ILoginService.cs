using Shopera.Application.DTOs.User;

namespace Shopera.Application.IService.User
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto model);
    }
}