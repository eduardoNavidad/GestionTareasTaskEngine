using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Tasks.Commands.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateTaskHandler(ITaskRepository taskRepository, ICategoryRepository categoryRepository,IMapper mapper)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
   
    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
        {
            throw new Exception("La categoría especificada no existe.");
        }
        var newTask = _mapper.Map<TaskItem>(request);

        await _taskRepository.AddAsync(newTask);

        return _mapper.Map<TaskDto>(newTask);
    }
}
