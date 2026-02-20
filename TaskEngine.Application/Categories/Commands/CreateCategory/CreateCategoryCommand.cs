using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;