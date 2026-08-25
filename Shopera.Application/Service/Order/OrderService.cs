using Shopera.Application.DTOs.Order;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Order;

namespace Shopera.Application.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = new Domain.Entities.Order
            {
                UserId = dto.UserId,
                TotalPrice = 0,
                CreateDate = DateTime.Now
            };

            var result = await _orderRepository.AddAsync(order);

            return new OrderDto
            {
                Id = result.Id,
                UserId = result.UserId,
                TotalPrice = result.TotalPrice,
                CreateDate = result.CreateDate
            };
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(x => new OrderDto
            {
                Id = x.Id,
                UserId = x.UserId,
                TotalPrice = x.TotalPrice,
                CreateDate = x.CreateDate
            }).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                CreateDate = order.CreateDate
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return false;

            await _orderRepository.DeleteAsync(order);

            return true;
        }
    }
}