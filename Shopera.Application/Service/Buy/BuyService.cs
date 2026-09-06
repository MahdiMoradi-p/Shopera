using Shopera.Application.DTOs.Buy;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Buy;
using Shopera.Domain.Enums;

namespace Shopera.Application.Service.Buy
{
    public class BuyService : IBuyService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public BuyService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceDetailRepository invoiceDetailRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<CheckoutResultDto> CheckoutAsync(string userId)
        {
            // 1. گرفتن سبد خرید کاربر
            var cart = await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
                throw new Exception("Cart not found.");

            // 2. گرفتن آیتم‌های سبد خرید
            var cartItems =
                await _cartItemRepository.GetByCartIdAsync(cart.Id);

            if (cartItems == null || cartItems.Count == 0)
                throw new Exception("Cart is empty.");

            // 3. بررسی موجودی و محاسبه مبلغ کل
            decimal totalPrice = 0;

            foreach (var item in cartItems)
            {
                var product =
                    await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception(
                        $"Product with id {item.ProductId} not found.");

                if (item.Quantity <= 0)
                    throw new Exception("Invalid quantity.");

                if (product.Stock < item.Quantity)
                    throw new Exception(
                        $"Not enough stock for product: {product.Title}");

                totalPrice += product.Price * item.Quantity;
            }

            // 4. ساخت Order
            var order = new Shopera.Domain.Entities.Order
            {
                UserId = userId,
                TotalPrice = totalPrice,
                CreateDate = DateTime.Now
            };

            var createdOrder =
                await _orderRepository.AddAsync(order);

            // 5. ساخت OrderDetail ها و کاهش موجودی
            foreach (var item in cartItems)
            {
                var product =
                    await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found.");

                var orderDetail =
                    new Shopera.Domain.Entities.OrderDetail
                    {
                        OrderId = createdOrder.Id,
                        ProductId = product.Id,
                        Count = item.Quantity,
                        Price = product.Price
                    };

                await _orderDetailRepository.AddAsync(orderDetail);

                // کاهش موجودی
                product.Stock -= item.Quantity;

                await _productRepository.UpdateAsync(product);
            }

            // 6. ساخت Invoice
            var invoice =
                new Shopera.Domain.Entities.Invoice
                {
                    OrderId = createdOrder.Id,
                    TotalAmount = totalPrice,
                    InvoiceDate = DateTime.Now,
                    Status = InvoiceStatus.Paid
                };

            var createdInvoice =
                await _invoiceRepository.AddAsync(invoice);

            // 7. ساخت InvoiceDetail ها
            foreach (var item in cartItems)
            {
                var product =
                    await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found.");

                var invoiceDetail =
                    new Shopera.Domain.Entities.InvoiceDetail
                    {
                        InvoiceId = createdInvoice.Id,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        TotalPrice = product.Price * item.Quantity
                    };

                await _invoiceDetailRepository.AddAsync(
                    invoiceDetail);
            }

            // 8. خالی کردن سبد خرید
            await _cartItemRepository.DeleteAllAsync(cart.Id);

            // 9. برگرداندن نتیجه خرید
            return new CheckoutResultDto
            {
                OrderId = createdOrder.Id,
                InvoiceId = createdInvoice.Id,
                TotalPrice = totalPrice
            };
        }
    }
}