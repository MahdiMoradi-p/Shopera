using Shopera.Domain.Entities;

namespace Shopera.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> AddAsync(Invoice invoice);

        Task<List<Invoice>> GetAllAsync();

        Task<Invoice?> GetByIdAsync(int id);

        Task UpdateAsync(Invoice invoice);

        Task DeleteAsync(Invoice invoice);
    }
}