using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByIdAsync(request.Id);
        if (userExists == null) 
            throw new Exception("User not found");

        _mapper.Map(request, userExists);

        await _userRepository.UpdateAsync(userExists);

        return _mapper.Map<UserDto>(userExists);

    }
}
