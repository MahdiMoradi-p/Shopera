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

        // ایجاد محصول
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);

            return Ok(product);
        }

        // دریافت همه محصولات
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        // جست‌وجوی محصولات
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("عبارت جست‌وجو الزامی است.");

            var products = await _productService.SearchAsync(query);

            return Ok(products);
        }

        // دریافت محصول با شناسه
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // ویرایش محصول
        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateProductDto dto)
        {
            var result = await _productService.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        // حذف محصول
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}