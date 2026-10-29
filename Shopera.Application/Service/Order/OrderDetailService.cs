using Shopera.Application.DTOs.Order;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Order;

namespace Shopera.Application.Service.Order
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _detailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderDetailService(
            IOrderDetailRepository detailRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _detailRepository = detailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDetailDto> CreateAsync(
            CreateOrderDetailDto dto)
        {
            var order =
                await _orderRepository.GetByIdAsync(dto.OrderId);

            if (order == null)
                throw new Exception("Order not found");

            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            var detail = new Domain.Entities.OrderDetail
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Count = dto.Count,
                Price = product.Price
            };

            var result =
                await _detailRepository.AddAsync(detail);

            order.TotalPrice += product.Price * dto.Count;

            await _orderRepository.UpdateAsync(order);

            return new OrderDetailDto
            {
                Id = result.Id,
                OrderId = result.OrderId,
                ProductId = result.ProductId,
                Count = result.Count,
                Price = result.Price
            };
        }

        public async Task<List<OrderDetailDto>> GetAllAsync()
        {
            var details =
                await _detailRepository.GetAllAsync();

            return details.Select(x => new OrderDetailDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Count = x.Count,
                Price = x.Price
            }).ToList();
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);

            if (detail == null)
                return null;

            return new OrderDetailDto
            {
                Id = detail.Id,
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Count = detail.Count,
                Price = detail.Price
            };
        }

        public async Task<List<OrderDetailDto>> GetByOrderIdAsync(
            int orderId)
        {
            var details =
                await _detailRepository.GetByOrderIdAsync(orderId);

            return details.Select(x => new OrderDetailDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Count = x.Count,
                Price = x.Price
            }).ToList();
        }

        public async Task<bool> UpdateAsync(
            UpdateOrderDetailDto dto)
        {
            var detail =
                await _detailRepository.GetByIdAsync(dto.Id);

            if (detail == null)
                return false;

            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            detail.ProductId = dto.ProductId;
            detail.Count = dto.Count;
            detail.Price = product.Price;

            await _detailRepository.UpdateAsync(detail);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);
            
            if (detail == null)
                return false;

            await _detailRepository.DeleteAsync(detail);

            return true;
        }
    }
}