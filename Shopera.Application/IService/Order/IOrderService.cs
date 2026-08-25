using Shopera.Application.DTOs.Order;

namespace Shopera.Application.IService.Order
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto dto);

        Task<List<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}