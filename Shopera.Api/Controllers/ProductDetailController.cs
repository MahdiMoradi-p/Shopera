using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.Product;
using Shopera.Application.IService.Product;

namespace Shopera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _service;

        public ProductDetailController(
            IProductDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDetailDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(
            int productId)
        {
            var result =
                await _service.GetByProductIdAsync(productId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateProductDetailDto dto)
        {
            var result =
                await _service.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}