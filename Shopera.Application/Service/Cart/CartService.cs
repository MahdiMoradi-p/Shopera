using Shopera.Application.DTOs.Cart;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Cart;
using Shopera.Domain.Entities;

namespace Shopera.Application.Service.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<CartDto> AddToCartAsync(
            string userId,
            AddToCartDto dto)
        {
            // 1. بررسی محصول
            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            // 2. بررسی تعداد
            if (dto.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            // 3. بررسی موجودی
            if (product.Stock < dto.Quantity)
                throw new Exception("Not enough stock.");

            // 4. پیدا کردن سبد کاربر
            var cart =
                await _cartRepository.GetByUserIdAsync(userId);

            // 5. اگر سبد نداشت، بساز
            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    UserId = userId
                };

                cart = await _cartRepository.AddAsync(cart);
            }

            // 6. بررسی اینکه محصول قبلاً داخل سبد هست یا نه
            var cartItem =
                await _cartItemRepository
                    .GetByCartAndProductAsync(
                        cart.Id,
                        dto.ProductId);

            if (cartItem != null)
            {
                // محصول قبلاً در سبد وجود دارد
                if (cartItem.Quantity + dto.Quantity > product.Stock)
                    throw new Exception("Not enough stock.");

                cartItem.Quantity += dto.Quantity;

                await _cartItemRepository.UpdateAsync(cartItem);
            }
            else
            {
                // محصول جدید است
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                await _cartItemRepository.AddAsync(cartItem);
            }

            // 7. برگرداندن سبد به‌روز شده
            return await GetCartAsync(userId);
        }

        public async Task<CartDto?> GetCartAsync(
            string userId)
        {
            // 1. پیدا کردن Cart
            var cart =
                await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
                return null;

            // 2. گرفتن آیتم‌های Cart
            var items =
                await _cartItemRepository
                    .GetByCartIdAsync(cart.Id);

            // 3. تبدیل به DTO
            var cartItems = items.Select(x => new CartItemDto
            {
                CartItemId = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Title,
                Quantity = x.Quantity,
                UnitPrice = x.Product.Price,
                TotalPrice = x.Product.Price * x.Quantity
            }).ToList();

            // 4. محاسبه قیمت کل
            var totalPrice =
                cartItems.Sum(x => x.TotalPrice);

            return new CartDto
            {
                CartId = cart.Id,
                Items = cartItems,
                TotalPrice = totalPrice
            };
        }

        public async Task<bool> UpdateCartItemAsync(
            string userId,
            UpdateCartItemDto dto)
        {
            // 1. پیدا کردن Cart کاربر
            var cart =
                await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
                return false;

            // 2. پیدا کردن CartItem
            var cartItem =
                await _cartItemRepository.GetByIdAsync(
                    dto.CartItemId);

            if (cartItem == null)
                return false;

            // 3. مطمئن شو آیتم متعلق به همین کاربر است
            if (cartItem.CartId != cart.Id)
                return false;

            // 4. بررسی Quantity
            if (dto.Quantity <= 0)
                return false;

            // 5. پیدا کردن Product
            var product =
                await _productRepository.GetByIdAsync(
                    cartItem.ProductId);

            if (product == null)
                return false;

            // 6. بررسی موجودی
            if (dto.Quantity > product.Stock)
                return false;

            // 7. تغییر تعداد
            cartItem.Quantity = dto.Quantity;

            await _cartItemRepository.UpdateAsync(cartItem);

            return true;
        }

        public async Task<bool> RemoveCartItemAsync(
            string userId,
            int cartItemId)
        {
            // 1. پیدا کردن Cart کاربر
            var cart =
                await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
                return false;

            // 2. پیدا کردن CartItem
            var cartItem =
                await _cartItemRepository.GetByIdAsync(
                    cartItemId);

            if (cartItem == null)
                return false;

            // 3. بررسی مالکیت
            if (cartItem.CartId != cart.Id)
                return false;

            // 4. حذف
            await _cartItemRepository.DeleteAsync(cartItem);

            return true;
        }

        public async Task<bool> ClearCartAsync(
            string userId)
        {
            // 1. پیدا کردن Cart کاربر
            var cart =
                await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
                return false;

            // 2. حذف تمام آیتم‌ها
            await _cartItemRepository
                .DeleteAllAsync(cart.Id);

            return true;
        }
    }
}