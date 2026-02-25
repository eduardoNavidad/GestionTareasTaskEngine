using TaskEngine.Domain.Entities;

namespace TaskEngine.Domain.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem,Guid>
    {

    }
}
