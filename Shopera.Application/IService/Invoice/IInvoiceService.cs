using Shopera.Application.DTOs.Invoice;

namespace Shopera.Application.IService.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto);

        Task<List<InvoiceDto>> GetAllAsync();

        Task<InvoiceDto?> GetByIdAsync(int id);

        Task<bool> UpdateStatusAsync(
            int id,
            Domain.Enums.InvoiceStatus status);

        Task<bool> DeleteAsync(int id);
    }
}