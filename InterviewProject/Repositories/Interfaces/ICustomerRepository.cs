using InterviewProject.Entities;

namespace InterviewProject.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public Task<IEnumerable<Customer>> SearchByNameAsync(string name);

    }
}
