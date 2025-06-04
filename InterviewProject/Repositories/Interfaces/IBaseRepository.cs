using InterviewProject.Entities;

namespace InterviewProject.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);
    }
}
