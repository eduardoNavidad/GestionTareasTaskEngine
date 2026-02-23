using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private  readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.Password = _passwordService.HashPassword(request.Password);

        await _userRepository.AddAsync(user);

        return _mapper.Map<UserDto>(user);  
    }
}
