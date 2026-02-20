using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Entities;

namespace TaskEngine.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
    
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasOne(t => t.Category)       // Una tarea tiene una categoría
                  .WithMany(c => c.Tasks)        // Una categoría tiene muchas tareas
                  .HasForeignKey(t => t.CategoryId) // La clave foránea es CategoryId
                  .IsRequired();                 // <--- Esto fuerza el INNER JOIN
        });
    }
}
