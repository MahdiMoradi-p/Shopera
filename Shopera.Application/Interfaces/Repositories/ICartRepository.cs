using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserIdAsync(string userId);

        Task<Cart?> GetByIdAsync(int id);

        Task<Cart> AddAsync(Cart cart);

        Task UpdateAsync(Cart cart);
    }
}