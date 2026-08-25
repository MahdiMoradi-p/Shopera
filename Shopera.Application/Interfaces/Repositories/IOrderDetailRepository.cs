using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> AddAsync(OrderDetail detail);

        Task<OrderDetail?> GetByIdAsync(int id);

        Task<List<OrderDetail>> GetAllAsync();

        Task<List<OrderDetail>> GetByOrderIdAsync(int orderId);

        Task UpdateAsync(OrderDetail detail);

        Task DeleteAsync(OrderDetail detail);
    }
}