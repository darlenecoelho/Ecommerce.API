using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;

namespace Ecommerce.API.Application.Queries.Category;

public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, ReadCategoryDTO>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadCategoryDTO> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByNameAsync(request.Name);

        if (category == null)
        {
            return null;
        }

        return _mapper.Map<ReadCategoryDTO>(category);
    }
}
