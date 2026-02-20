using MediatR;
using TaskEngine.Application.DTOs;

namespace TaskEngine.Application.Categories.Queries.GetCategoryById;

public record class GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;
