using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.Invoice;
using Shopera.Application.IService.Invoice;
using Shopera.Domain.Enums;

namespace Shopera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateInvoiceDto dto)
        {
            var result =
                await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            InvoiceStatus status)
        {
            var result =
                await _service.UpdateStatusAsync(id, status);

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