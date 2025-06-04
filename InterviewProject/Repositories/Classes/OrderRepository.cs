using InterviewProject.Data;
using InterviewProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Repositories.Interfaces
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InterviewContext _context;

        public OrderRepository(InterviewContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var entity = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(true);

            return entity ?? throw new KeyNotFoundException($"Order with id {id} not found");
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity)
                .ConfigureAwait(true);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id)
                .ConfigureAwait(true);
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Orders
                .AnyAsync(o => o.Id == id)
                .ConfigureAwait(true);
        }

        public async Task<Order> GetByOrderNoAsync(int orderNo)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderNo == orderNo)
                .ConfigureAwait(true)
                ?? throw new KeyNotFoundException($"Order with number {orderNo} not found");
        }

        public async Task<IEnumerable<Order>> GetOrdersWithCommissionsAsync()
        {
            return await _context.Orders
                .Include(o => o.PurchaseCommissions)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<Order> GetOrderWithFullDetailsAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.PurchaseCommissions)
                    .ThenInclude(pc => pc.PurchaseCommissionCustomers)
                        .ThenInclude(pcc => pcc.Seller)
                .FirstOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(true)
                ?? throw new KeyNotFoundException($"Order with id {id} not found");
        }
    }
}
