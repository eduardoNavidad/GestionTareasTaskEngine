using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Tasks.Queries.GetAllTasks;

public record class GetTaskQuery() : IRequest<List<TaskDto>>;
