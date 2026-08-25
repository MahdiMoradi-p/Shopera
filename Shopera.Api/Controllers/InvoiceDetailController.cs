using Microsoft.AspNetCore.Mvc;
using Shopera.Application.DTOs.Invoice;
using Shopera.Application.IService.Invoice;

namespace Shopera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _service;

        public InvoiceDetailController(
            IInvoiceDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateInvoiceDetailDto dto)
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

        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoiceId(
            int invoiceId)
        {
            var result =
                await _service.GetByInvoiceIdAsync(invoiceId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateInvoiceDetailDto dto)
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