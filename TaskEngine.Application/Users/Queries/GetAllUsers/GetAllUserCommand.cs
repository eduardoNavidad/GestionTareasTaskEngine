using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Users.Queries.GetAllUsers;

public record class GetAllUserCommand() : IRequest<List<UserDto>>;
