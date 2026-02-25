using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskEngine.Application.Categories.Commands.CreateCategory;
using TaskEngine.Application.Categories.Commands.DeleteCategory;
using TaskEngine.Application.Categories.Commands.UpdateCategory;
using TaskEngine.Application.Categories.Queries.GetAllCategory;
using TaskEngine.Application.Categories.Queries.GetCategoryById;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Queries.GetAllTasks;

namespace TaskEngine.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)=> _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));
        return category is not null ? Ok(category) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> Update(Guid id, UpdateCategoryCommand command)
    {
        if (id != command.Id) return BadRequest("Los IDs no coinciden");

        var result = await _mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    { 
        var result  = await _mediator.Send(new DeleteCategoryCommand(id));  
        if (!result)
        {
            return NotFound($"No se pudo eliminar la categoría con ID: {id}. Es posible que ya no exista.");
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoryCommand());
        return Ok(result);
    }
}
