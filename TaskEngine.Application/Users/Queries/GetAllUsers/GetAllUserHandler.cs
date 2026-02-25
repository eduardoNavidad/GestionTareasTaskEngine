using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Users.Queries.GetAllUsers;

public class GetAllUserHandler : IRequestHandler<GetAllUserCommand, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }
}
