using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Users.Commands.CreateUser;
using TaskEngine.Application.Users.Commands.DeleteUser;
using TaskEngine.Application.Users.Commands.UpdateUser;
using TaskEngine.Application.Users.Queries.GetAllUsers;
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

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(int id, UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest("Los IDs no coinciden");
        var result = await _mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        if (!result)
        {
            return NotFound($"No se pudo eliminar el usuario con ID: {id}. Es posible que ya no exista.");
        }

        return NoContent(); // Placeholder, implementa la lógica de eliminación según tu aplicación.
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUserCommand());
        return Ok(result);
    }

}
