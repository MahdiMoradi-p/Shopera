using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);

        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> SearchAsync(string query);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}