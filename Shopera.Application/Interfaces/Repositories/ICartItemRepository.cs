using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddAsync(CartItem cartItem);

        Task<CartItem?> GetByIdAsync(int id);

        Task<CartItem?> GetByCartAndProductAsync(
            int cartId,
            int productId);

        Task<List<CartItem>> GetByCartIdAsync(
            int cartId);

        Task UpdateAsync(CartItem cartItem);

        Task DeleteAsync(CartItem cartItem);

        Task DeleteAllAsync(int cartId);
    }
}