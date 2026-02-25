using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;
namespace TaskEngine.Infrastructure.Repositories
{
    public class TaskRepository : Repository<TaskItem,Guid>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) : base( context){ _context = context;  }  

        public override async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems
                    .Include(t => t.Category) // <--- Esto carga los datos de la categoría relacionada
                    .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
