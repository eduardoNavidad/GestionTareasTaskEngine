using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Categories.Commands.UpdateCategory;

public record class UpdateCategoryCommand(Guid Id, string Name) : IRequest<CategoryDto>;
