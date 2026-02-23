using TaskEngine.Domain.Entities;

namespace TaskEngine.Domain.Interfaces; 

public interface IJwtServiceRepository
{
    Task<LoginResponseModel?> Authenticate(LoginRequestModel request);
}
