using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Subcategory;

public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, ReadSubcategoryDTO>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSubcategoryByIdQueryHandler> _logger;

    public GetSubcategoryByIdQueryHandler(
        ISubcategoryRepository subcategoryRepository,
        IMapper mapper,
        ILogger<GetSubcategoryByIdQueryHandler> logger)
    {
        _subcategoryRepository = subcategoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReadSubcategoryDTO> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando subcategoria com ID: {Id}", request.Id);

        var subcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(request.Id);

        if (subcategory == null)
        {
            throw new Exception("Subcategoria não encontrada. Verifique o ID informado.");
        }

        return _mapper.Map<ReadSubcategoryDTO>(subcategory);
    }
}
