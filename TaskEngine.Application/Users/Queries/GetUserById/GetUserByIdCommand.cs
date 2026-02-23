using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Users.Queries.GetUserById;
public record class GetUserByIdCommand(int id) : IRequest<UserDto?>;
