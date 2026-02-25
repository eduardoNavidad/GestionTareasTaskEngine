using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;

namespace TaskEngine.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context){ _context = context; } 
        
    }
}
