using TaskEngine.Domain.Entities;

namespace TaskEngine.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task <IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

        Task AddAsync(Category category);
    }
}
