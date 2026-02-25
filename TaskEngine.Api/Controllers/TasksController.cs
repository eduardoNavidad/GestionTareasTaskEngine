using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Commands.CreateTask;
using TaskEngine.Application.Tasks.Commands.DeleteTask;
using TaskEngine.Application.Tasks.Commands.UpdateTask;
using TaskEngine.Application.Tasks.Queries.GetAllTasks;
using TaskEngine.Application.Tasks.Queries.GetTaskById;

namespace TaskEngine.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : Controller
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id =  result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> Update(Guid id,UpdateTaskCommand command)
    {
        if (id != command.Id) return BadRequest("Los IDs no coinciden");

        var result = await _mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id));
        if (!result)
        {
            return NotFound($"No se pudo eliminar la tarea con ID: {id}. Es posible que ya no exista.");
        }

        // 204 No Content es la respuesta estándar para un DELETE exitoso
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _mediator.Send(new GetTaskQuery());
        return Ok(tasks);
    }

}
