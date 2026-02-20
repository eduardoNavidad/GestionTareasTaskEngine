using MediatR;
using TaskEngine.Application.DTOs;


namespace TaskEngine.Application.Tasks.Queries.GetTaskById;

public record class GetTaskByIdQuery(Guid Id) : IRequest<TaskDto?>;

