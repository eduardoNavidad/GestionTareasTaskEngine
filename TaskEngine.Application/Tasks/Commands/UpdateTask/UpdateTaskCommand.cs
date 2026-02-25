using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Tasks.Commands.UpdateTask;

public record class UpdateTaskCommand(Guid Id, string Title, string Description, bool IsCompleted, Guid CategoryId) : IRequest<TaskDto>;
