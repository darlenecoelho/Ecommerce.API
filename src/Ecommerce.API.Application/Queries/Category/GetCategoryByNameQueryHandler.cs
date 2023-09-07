using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Category;

public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, ReadCategoryDTO>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryByNameQueryHandler> _logger;

    public GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetCategoryByNameQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReadCategoryDTO> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByNameAsync(request.Name);

        if (category == null)
        {
            var errorMessage = "Categoria não encontrada. Verifique o nome informado";
            _logger.LogWarning(errorMessage);
            throw new Exception(errorMessage);
        }

        return _mapper.Map<ReadCategoryDTO>(category);
    }
}
