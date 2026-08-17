using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);

        Task<Order?> GetByIdAsync(int id);

        Task<List<Order>> GetAllAsync();

        Task UpdateAsync(Order order);

        Task DeleteAsync(Order order);
    }
}