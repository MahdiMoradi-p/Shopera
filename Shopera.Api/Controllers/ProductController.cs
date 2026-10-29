using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.Product;
using Shopera.Application.IService.Product;

namespace Shopera.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // فقط Admin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var product =
                await _productService.CreateAsync(dto);

            return Ok(product);
        }

        // Admin و User
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products =
                await _productService.GetAllAsync();

            return Ok(products);
        }

        // Admin و User
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product =
                await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // فقط Admin
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateProductDto dto)
        {
            var result =
                await _productService.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // فقط Admin
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _productService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}