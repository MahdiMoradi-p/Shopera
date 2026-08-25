using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IProductDetailRepository
    {
        Task<ProductDetail> AddAsync(ProductDetail productDetail);

        Task<List<ProductDetail>> GetAllAsync();

        Task<ProductDetail?> GetByIdAsync(int id);

        Task<List<ProductDetail>> GetByProductIdAsync(int productId);

        Task UpdateAsync(ProductDetail productDetail);

        Task DeleteAsync(ProductDetail productDetail);
    }
}