using Microsoft.EntityFrameworkCore;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;

namespace Shopera.Infrastructure.Repositories
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceDetail> AddAsync(InvoiceDetail detail)
        {
            await _context.InvoiceDetails.AddAsync(detail);
            await _context.SaveChangesAsync();

            return detail;
        }

        public async Task<List<InvoiceDetail>> GetAllAsync()
        {
            return await _context.InvoiceDetails
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<InvoiceDetail?> GetByIdAsync(int id)
        {
            return await _context.InvoiceDetails
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<InvoiceDetail>> GetByInvoiceIdAsync(
            int invoiceId)
        {
            return await _context.InvoiceDetails
                .Where(x => x.InvoiceId == invoiceId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task UpdateAsync(InvoiceDetail detail)
        {
            _context.InvoiceDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(InvoiceDetail detail)
        {
            _context.InvoiceDetails.Remove(detail);
            await _context.SaveChangesAsync();
        }
    }
}