using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;

namespace Ecommerce.API.Application.Queries.Category;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<ReadCategoryDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadCategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return _mapper.Map<List<ReadCategoryDTO>>(categories);
    }
}

