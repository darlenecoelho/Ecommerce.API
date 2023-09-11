using AutoMapper;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Product;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ReadProductDTO>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ReadProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetAllProductsAsync();

            var productDTOs = _mapper.Map<List<ReadProductDTO>>(products);

            return productDTOs.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao buscar todos os produtos: {ex.Message}");
            throw new Exception("Ocorreu um erro ao buscar todos os produtos.");
        }
    }
}
