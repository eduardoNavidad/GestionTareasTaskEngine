using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Tasks.Commands.CreateTask;

public record class CreateTaskCommand(string Title, string Description, DateTime DueDate, Guid CategoryId) : IRequest<TaskDto>;

