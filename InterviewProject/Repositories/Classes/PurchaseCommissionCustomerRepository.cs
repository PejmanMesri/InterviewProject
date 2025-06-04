using InterviewProject.Data;
using InterviewProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Repositories.Interfaces
{

    public class PurchaseCommissionCustomerRepository : IPurchaseCommissionCustomerRepository
    {
        private readonly InterviewContext _context;

        public PurchaseCommissionCustomerRepository(InterviewContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PurchaseCommissionCustomer> GetByIdAsync(Guid id)
        {
            var entity = await _context.PurchaseCommissionCustomers
                .Include(pcc => pcc.PurchaseCommission)
                .Include(pcc => pcc.Seller)
                .FirstOrDefaultAsync(pcc => pcc.Id == id)
                .ConfigureAwait(true);

            return entity ?? throw new KeyNotFoundException($"PurchaseCommissionCustomer with id {id} not found");
        }

        public async Task<IEnumerable<PurchaseCommissionCustomer>> GetAllAsync()
        {
            return await _context.PurchaseCommissionCustomers
                .Include(pcc => pcc.PurchaseCommission)
                .Include(pcc => pcc.Seller)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task AddAsync(PurchaseCommissionCustomer entity)
        {
            await _context.PurchaseCommissionCustomers.AddAsync(entity)
                .ConfigureAwait(true);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task UpdateAsync(PurchaseCommissionCustomer entity)
        {
            _context.PurchaseCommissionCustomers.Update(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id)
                .ConfigureAwait(true);
            _context.PurchaseCommissionCustomers.Remove(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.PurchaseCommissionCustomers
                .AnyAsync(pcc => pcc.Id == id)
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommissionCustomer>> GetByPurchaseCommissionIdAsync(Guid purchaseCommissionId)
        {
            return await _context.PurchaseCommissionCustomers
                .Where(pcc => pcc.PurchaseCommissionId == purchaseCommissionId)
                .Include(pcc => pcc.Seller)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommissionCustomer>> GetBySellerIdAsync(Guid sellerId)
        {
            return await _context.PurchaseCommissionCustomers
                .Where(pcc => pcc.SellerId == sellerId)
                .Include(pcc => pcc.PurchaseCommission)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommissionCustomer>> GetWithDetailsAsync()
        {
            return await _context.PurchaseCommissionCustomers
                .Include(pcc => pcc.PurchaseCommission)
                    .ThenInclude(pc => pc.Order)
                .Include(pcc => pcc.Seller)
                .ToListAsync()
                .ConfigureAwait(true);
        }
    }
}
