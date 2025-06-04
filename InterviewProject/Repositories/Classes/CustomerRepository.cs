using InterviewProject.Data;
using InterviewProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Repositories.Interfaces
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InterviewContext _context;

        public CustomerRepository(InterviewContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var entity = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(true);

            return entity ?? throw new KeyNotFoundException($"Customer with id {id} not found");
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity)
                .ConfigureAwait(true);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id)
                .ConfigureAwait(true);
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Customers
                .AnyAsync(c => c.Id == id)
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<Customer>> SearchByNameAsync(string name)
        {
            return await _context.Customers
                .Where(c => c.Name.Contains(name))
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithCommissionsAsync()
        {
            return await _context.Customers
                .Include(c => c.PurchaseCommissionCustomers)
                    .ThenInclude(pcc => pcc.PurchaseCommission)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<Customer> GetCustomerWithDetailsAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.PurchaseCommissionCustomers)
                    .ThenInclude(pcc => pcc.PurchaseCommission)
                        .ThenInclude(pc => pc.Order)
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(true)
                ?? throw new KeyNotFoundException($"Customer with id {id} not found");
        }
    }
}
