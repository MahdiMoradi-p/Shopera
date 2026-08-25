using Shopera.Application.DTOs.Order;

namespace Shopera.Application.IService.Order
{
    public interface IOrderDetailService
    {
        Task<OrderDetailDto> CreateAsync(
            CreateOrderDetailDto dto);

        Task<List<OrderDetailDto>> GetAllAsync();

        Task<OrderDetailDto?> GetByIdAsync(int id);

        Task<List<OrderDetailDto>> GetByOrderIdAsync(
            int orderId);

        Task<bool> UpdateAsync(
            UpdateOrderDetailDto dto);

        Task<bool> DeleteAsync(int id);
    }
}