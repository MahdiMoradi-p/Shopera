using Shopera.Application.DTOs.Cart;

namespace Shopera.Application.IService.Cart
{
    public interface ICartService
    {
        Task<CartDto> AddToCartAsync(
            string userId,
            AddToCartDto dto);

        Task<CartDto?> GetCartAsync(
            string userId);

        Task<bool> UpdateCartItemAsync(
            string userId,
            UpdateCartItemDto dto);

        Task<bool> RemoveCartItemAsync(
            string userId,
            int cartItemId);

        Task<bool> ClearCartAsync(
            string userId);
    }
}