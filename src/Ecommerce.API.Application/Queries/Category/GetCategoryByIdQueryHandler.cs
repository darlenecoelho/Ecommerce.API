using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Category;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ReadCategoryDTO>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReadCategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando categoria por ID: {Id}", request.Id);

        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

        if (category == null)
        {
            var errorMessage = "Categoria não encontrada. Verifique o ID informado";
            _logger.LogWarning(errorMessage);
            throw new Exception(errorMessage);
        }

        return _mapper.Map<ReadCategoryDTO>(category);
    }
}