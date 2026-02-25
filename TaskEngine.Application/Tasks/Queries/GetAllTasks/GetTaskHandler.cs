using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Tasks.Queries.GetAllTasks;

public class GetTaskHandler : IRequestHandler<GetTaskQuery, List<TaskDto>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetTaskHandler(IMapper mapper,ITaskRepository taskRepository)
    { 
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskDto>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();
        return _mapper.Map<List<TaskDto>>(tasks);
    }
}
