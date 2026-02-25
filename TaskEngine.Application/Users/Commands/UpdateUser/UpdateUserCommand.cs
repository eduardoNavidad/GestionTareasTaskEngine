using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Users.Commands.UpdateUser;

public record class UpdateUserCommand(int Id, string Name, string UserName) : IRequest<UserDto>;
