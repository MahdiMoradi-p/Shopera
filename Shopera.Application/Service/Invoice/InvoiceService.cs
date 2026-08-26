using Shopera.Application.DTOs.Invoice;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Invoice;
using Shopera.Domain.Enums;

namespace Shopera.Application.Service.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
        }

        public async Task<InvoiceDto> CreateAsync(
            CreateInvoiceDto dto)
        {
            var order =
                await _orderRepository.GetByIdAsync(dto.OrderId);

            if (order == null)
                throw new Exception("Order not found.");

            var invoice = new Domain.Entities.Invoice
            {
                OrderId = dto.OrderId,
                TotalAmount = order.TotalPrice,
                InvoiceDate = DateTime.Now,
                Status = InvoiceStatus.Pending
            };

            var result =
                await _invoiceRepository.AddAsync(invoice);

            return new InvoiceDto
            {
                Id = result.Id,
                OrderId = result.OrderId,
                TotalAmount = result.TotalAmount,
                InvoiceDate = result.InvoiceDate,
                Status = result.Status
            };
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            var invoices =
                await _invoiceRepository.GetAllAsync();

            return invoices.Select(x => new InvoiceDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                TotalAmount = x.TotalAmount,
                InvoiceDate = x.InvoiceDate,
                Status = x.Status
            }).ToList();
        }

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
                return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                OrderId = invoice.OrderId,
                TotalAmount = invoice.TotalAmount,
                InvoiceDate = invoice.InvoiceDate,
                Status = invoice.Status
            };
        }

        public async Task<bool> UpdateStatusAsync(
            int id,
            InvoiceStatus status)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
                return false;

            invoice.Status = status;

            await _invoiceRepository.UpdateAsync(invoice);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
                return false;

            await _invoiceRepository.DeleteAsync(invoice);

            return true;
        }
    }
}