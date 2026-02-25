using AutoMapper;
using MediatR;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByIdAsync(request.Id);

        if (userExists == null) 
            throw new Exception("User not found");

        var rowsAffected =  await _userRepository.DeleteAsync(userExists);

        return rowsAffected > 0;


    }
}
