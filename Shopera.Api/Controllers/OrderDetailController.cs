using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.Order;
using Shopera.Application.IService.Order;

namespace Shopera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(
            IOrderDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateOrderDetailDto dto)
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

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(
            int orderId)
        {
            var result =
                await _service.GetByOrderIdAsync(orderId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateOrderDetailDto dto)
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