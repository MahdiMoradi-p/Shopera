using Microsoft.EntityFrameworkCore;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;

namespace Shopera.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddAsync(
            CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);

            await _context.SaveChangesAsync();

            return cartItem;
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CartItem?> GetByCartAndProductAsync(
            int cartId,
            int productId)
        {
            return await _context.CartItems
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x =>
                    x.CartId == cartId &&
                    x.ProductId == productId);
        }

        public async Task<List<CartItem>> GetByCartIdAsync(
            int cartId)
        {
            return await _context.CartItems
                .Where(x => x.CartId == cartId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task UpdateAsync(
            CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(int cartId)
        {
            var items = await _context.CartItems
                .Where(x => x.CartId == cartId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);

            await _context.SaveChangesAsync();
        }
    }
}