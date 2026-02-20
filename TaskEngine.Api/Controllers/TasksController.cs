using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Commands.CreateTask;
using TaskEngine.Application.Tasks.Queries.GetTaskById;

namespace TaskEngine.Api.Controllers;

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
        //Console.WriteLine($"Received request to get task with ID: {id}");
        //var tasks = await _mediator.Send(new GetTaskByIdQuery(id));
        //Console.WriteLine(tasks);
        //if (tasks == null) return NotFound();

        //return Ok(tasks);
        var result = await _mediator.Send(new GetTaskByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }
}
