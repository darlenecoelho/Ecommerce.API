using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Subcategory;

public class GetAllSubcategoriesQueryHandler : IRequestHandler<GetAllSubcategoriesQuery, List<ReadSubcategoryDTO>>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSubcategoriesQueryHandler> _logger;

    public GetAllSubcategoriesQueryHandler(ISubcategoryRepository subcategoryRepository, IMapper mapper, ILogger<GetAllSubcategoriesQueryHandler> logger)
    {
        _subcategoryRepository = subcategoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ReadSubcategoryDTO>> Handle(GetAllSubcategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando todas as subcategorias");
        var subcategories = await _subcategoryRepository.GetAllSubcategoriesAsync();
        var subcategoryDTOs = _mapper.Map<List<ReadSubcategoryDTO>>(subcategories);
        _logger.LogInformation("Total de subcategorias obtidas: {Count}", subcategories.Count);
        return subcategoryDTOs;
    }
}
