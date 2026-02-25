using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Categories.Queries.GetAllCategory;

public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryCommand, List<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public GetAllCategoryHandler(IMapper mapper, ICategoryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<CategoryDto>> Handle(GetAllCategoryCommand request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync();

        return _mapper.Map<List<CategoryDto>>(categories);

    }
}
