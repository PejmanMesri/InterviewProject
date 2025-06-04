using InterviewProject.Data;
using InterviewProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Repositories.Interfaces
{
    public class PurchaseCommissionRepository : IPurchaseCommissionRepository
    {
        private readonly InterviewContext _context;

        public PurchaseCommissionRepository(InterviewContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PurchaseCommission> GetByIdAsync(Guid id)
        {
            var entity = await _context.PurchaseCommissions
                .Include(pc => pc.Order)
                .Include(pc => pc.PurchaseCommissionCustomers)
                .FirstOrDefaultAsync(pc => pc.Id == id)
                .ConfigureAwait(true);

            return entity ?? throw new KeyNotFoundException($"PurchaseCommission with id {id} not found");
        }

        public async Task<IEnumerable<PurchaseCommission>> GetAllAsync()
        {
            return await _context.PurchaseCommissions
                .Include(pc => pc.Order)
                .Include(pc => pc.PurchaseCommissionCustomers)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task AddAsync(PurchaseCommission entity)
        {
            //in case db id is not set automatically
            entity.Id = Guid.NewGuid();

            entity.CurrentDate = StaticMethods.ConvertPersianToGregorian(entity.PersianCurrentDate);
            entity.RequiredDate =StaticMethods.ConvertPersianToGregorian(entity.PersianRequiredDate);

            await _context.PurchaseCommissions.AddAsync(entity)
                .ConfigureAwait(true);

            if (entity.PurchaseCommissionCustomers != null)
            {
                foreach (var customer in entity.PurchaseCommissionCustomers)
                {
                    customer.Id = Guid.NewGuid(); 
                    customer.DeliveryDate = StaticMethods.ConvertPersianToGregorian(customer.PersianDeliveryDate);

                    if (!string.IsNullOrEmpty(customer.PersianLastPurchaseDate))
                    {
                        customer.LastPurchaseDate = StaticMethods.ConvertPersianToGregorian(customer.PersianLastPurchaseDate);
                    }
                }
            }

            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task UpdateAsync(PurchaseCommission entity)
        {
            _context.PurchaseCommissions.Update(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id)
                .ConfigureAwait(true);
            _context.PurchaseCommissions.Remove(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.PurchaseCommissions
                .AnyAsync(pc => pc.Id == id)
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommission>> GetByProductCodeAsync(int productCode)
        {
            return await _context.PurchaseCommissions
                .Where(pc => pc.ProductCode == productCode)
                .Include(pc => pc.Order)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommission>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.PurchaseCommissions
                .Where(pc => pc.OrderId == orderId)
                .Include(pc => pc.PurchaseCommissionCustomers)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommission>> GetCommissionsWithCustomersAsync()
        {
            return await _context.PurchaseCommissions
                .Include(pc => pc.PurchaseCommissionCustomers)
                    .ThenInclude(pcc => pcc.Seller)
                .Include(pc => pc.Order)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<IEnumerable<PurchaseCommission>> GetCommissionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.PurchaseCommissions
                .Where(pc => pc.CurrentDate >= startDate && pc.CurrentDate <= endDate)
                .Include(pc => pc.Order)
                .Include(pc => pc.PurchaseCommissionCustomers)
                .ToListAsync()
                .ConfigureAwait(true);
        }
    }
}
