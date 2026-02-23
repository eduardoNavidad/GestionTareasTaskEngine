using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Users.Commands.CreateUser;
using TaskEngine.Application.Users.Queries.GetUserById;

namespace TaskEngine.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdCommand(id));
        return result is not null ? Ok(result) : NotFound();
    }

}
