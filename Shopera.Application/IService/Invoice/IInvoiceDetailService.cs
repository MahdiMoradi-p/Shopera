using Shopera.Application.DTOs.Invoice;

namespace Shopera.Application.IService.Invoice
{
    public interface IInvoiceDetailService
    {
        Task<InvoiceDetailDto> CreateAsync(
            CreateInvoiceDetailDto dto);

        Task<List<InvoiceDetailDto>> GetAllAsync();

        Task<InvoiceDetailDto?> GetByIdAsync(int id);

        Task<List<InvoiceDetailDto>> GetByInvoiceIdAsync(
            int invoiceId);

        Task<bool> UpdateAsync(
            UpdateInvoiceDetailDto dto);

        Task<bool> DeleteAsync(int id);
    }
}