using MediatR;

namespace TaskEngine.Application.Categories.Commands.DeleteCategory;

public record class DeleteCategoryCommand(Guid Id) : IRequest<bool>;
