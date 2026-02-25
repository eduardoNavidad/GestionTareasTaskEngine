using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;

namespace TaskEngine.Infrastructure.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context) => _context = context;

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity); 
        await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
