namespace TaskEngine.Domain.Interfaces;

public interface IRepository<TEntity, TId> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity); 
    Task<int> DeleteAsync(TEntity entity); 

}
