using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Category;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<ReadCategoryDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetAllCategoriesQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ReadCategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando todas as categorias");

        var categories = await _categoryRepository.GetAllCategoriesAsync();

        _logger.LogInformation("Total de categorias obtidas: {Count}", categories.Count);

        return _mapper.Map<List<ReadCategoryDTO>>(categories);
    }
}

