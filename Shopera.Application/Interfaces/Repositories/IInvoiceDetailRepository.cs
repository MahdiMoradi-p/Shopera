using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IInvoiceDetailRepository
    {
        Task<InvoiceDetail> AddAsync(InvoiceDetail detail);

        Task<List<InvoiceDetail>> GetAllAsync();

        Task<InvoiceDetail?> GetByIdAsync(int id);

        Task<List<InvoiceDetail>> GetByInvoiceIdAsync(int invoiceId);

        Task UpdateAsync(InvoiceDetail detail);

        Task DeleteAsync(InvoiceDetail detail);
    }
}