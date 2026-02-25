using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    public readonly IMapper _mapper;
    public readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryRepository.GetByIdAsync(request.Id);

        if (categoryExists == null)
            throw new Exception("Category not found");

        _mapper.Map(request, categoryExists);

        await _categoryRepository.UpdateAsync(categoryExists);

        return _mapper.Map<CategoryDto>(categoryExists);


    }
}
