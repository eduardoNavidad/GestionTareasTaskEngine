using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Categories.Queries.GetAllCategory;

public record class GetAllCategoryCommand() : IRequest<List<CategoryDto>>;
