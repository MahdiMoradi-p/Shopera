using Microsoft.EntityFrameworkCore;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;

namespace Shopera.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(x => x.Order)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}