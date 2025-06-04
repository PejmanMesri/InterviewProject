using InterviewProject.Entities;

namespace InterviewProject.Repositories.Interfaces
{
    public interface IPurchaseCommissionCustomerRepository : IBaseRepository<PurchaseCommissionCustomer>
    {
        public Task<IEnumerable<PurchaseCommissionCustomer>> GetByPurchaseCommissionIdAsync(Guid purchaseCommissionId);
        public Task<IEnumerable<PurchaseCommissionCustomer>> GetWithDetailsAsync();

    }
}
