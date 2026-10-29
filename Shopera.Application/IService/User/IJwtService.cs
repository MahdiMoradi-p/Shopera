using Shopera.Domain.Entities;

namespace Shopera.Application.IService.User
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}