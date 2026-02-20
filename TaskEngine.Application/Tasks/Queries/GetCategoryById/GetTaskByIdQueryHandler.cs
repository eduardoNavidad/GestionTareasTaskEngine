using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Queries.GetTaskById;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Tasks.Queries.GetCategoryByIdHandler;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;   
    }
    public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)  return null;
        return _mapper.Map<TaskDto>(task);
        //var task = await _taskRepository.GetByIdAsync(request.Id);

        //if (task == null) return null;
        //return new TaskDto(
        //    task.Id,
        //    task.Title,
        //    task.Description,
        //    task.DueDate,
        //    task.IsCompleted,
        //    task.CategoryId,
        //    task.Category?.Name ?? "Sin Categoría"
        //);
    }
}
