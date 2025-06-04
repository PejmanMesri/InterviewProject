using InterviewProject.Entities;

namespace InterviewProject.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrdersWithCommissionsAsync();
        public Task<Order> GetOrderWithFullDetailsAsync(Guid id);
        public Task<Order> GetByOrderNoAsync(int orderNo);

    }
}
