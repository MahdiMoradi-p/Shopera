using Microsoft.EntityFrameworkCore;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;

namespace Shopera.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> AddAsync(
            OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);

            await _context.SaveChangesAsync();

            return detail;
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(int id)
        {
            return await _context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(
            int orderId)
        {
            return await _context.OrderDetails
                .Where(x => x.OrderId == orderId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task UpdateAsync(
            OrderDetail detail)
        {
            _context.OrderDetails.Update(detail);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            OrderDetail detail)
        {
            _context.OrderDetails.Remove(detail);

            await _context.SaveChangesAsync();
        }
    }
}