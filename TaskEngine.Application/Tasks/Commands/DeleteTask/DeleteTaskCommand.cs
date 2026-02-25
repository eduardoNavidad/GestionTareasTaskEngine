using MediatR;

namespace TaskEngine.Application.Tasks.Commands.DeleteTask;

public record class DeleteTaskCommand(Guid Id) : IRequest<bool>;
