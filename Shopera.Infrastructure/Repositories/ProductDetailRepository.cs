using Microsoft.EntityFrameworkCore;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;

namespace Shopera.Infrastructure.Repositories
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDetail> AddAsync(
            ProductDetail productDetail)
        {
            await _context.ProductDetails.AddAsync(productDetail);
            await _context.SaveChangesAsync();

            return productDetail;
        }

        public async Task<List<ProductDetail>> GetAllAsync()
        {
            return await _context.ProductDetails
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductDetail?> GetByIdAsync(int id)
        {
            return await _context.ProductDetails
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductDetail>> GetByProductIdAsync(
            int productId)
        {
            return await _context.ProductDetails
                .Where(x => x.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(
            ProductDetail productDetail)
        {
            _context.ProductDetails.Update(productDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            ProductDetail productDetail)
        {
            _context.ProductDetails.Remove(productDetail);
            await _context.SaveChangesAsync();
        }
    }
}