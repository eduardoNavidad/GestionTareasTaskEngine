using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Users.Commands.DeleteUser;

public record class DeleteUserCommand(int Id) : IRequest<bool>;
