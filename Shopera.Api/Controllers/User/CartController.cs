using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shopera.Application.DTOs.Cart;
using Shopera.Application.IService.Cart;

namespace Shopera.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(
            AddToCartDto dto)
        {
            var userId = GetUserId();

            var result = await _cartService.AddToCartAsync(
                userId,
                dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();

            var result = await _cartService.GetCartAsync(userId);

            if (result == null)
                return NotFound("Cart not found.");

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem(
            UpdateCartItemDto dto)
        {
            var userId = GetUserId();

            var result =
                await _cartService.UpdateCartItemAsync(
                    userId,
                    dto);

            if (!result)
                return BadRequest(
                    "Cart item could not be updated.");

            return NoContent();
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(
            int cartItemId)
        {
            var userId = GetUserId();

            var result =
                await _cartService.RemoveCartItemAsync(
                    userId,
                    cartItemId);

            if (!result)
                return NotFound(
                    "Cart item not found.");

            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            var result =
                await _cartService.ClearCartAsync(userId);

            if (!result)
                return NotFound("Cart not found.");

            return NoContent();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;
        }
    }
}