using MediatR;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new Exception("Category not exists");

        int rowsAffected = await _categoryRepository.DeleteAsync(category);

        return rowsAffected > 0;

    }
}
