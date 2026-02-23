
using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Users.Commands.CreateUser;

public record class CreateUserCommand(string Name, string UserName, string Password) : IRequest<UserDto>;
