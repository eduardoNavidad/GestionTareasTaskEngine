using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Api.Controllers;

public class AuthController : ControllerBase
{
    private readonly IJwtServiceRepository _jwtServiceRepository;

    public AuthController(IJwtServiceRepository jwtServiceRepository)
    {
        _jwtServiceRepository = jwtServiceRepository;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
    {
        var result = await _jwtServiceRepository.Authenticate(request);
        if (result is null)
            return Unauthorized();

        return result;

    }

}
