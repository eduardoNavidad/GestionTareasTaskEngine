using AutoMapper;
using MediatR;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Tasks.Commands.DeleteTask;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IMapper _mapper;

    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(IMapper mapper, ITaskRepository taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }
    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskExisting = await _taskRepository.GetByIdAsync(request.Id);
        if (taskExisting == null)
            throw new Exception("La tarea no existe.");

        int rowsAffected = await _taskRepository.DeleteAsync(taskExisting);

        return rowsAffected > 0 ;

    }
}
