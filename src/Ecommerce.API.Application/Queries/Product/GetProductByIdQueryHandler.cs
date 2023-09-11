using AutoMapper;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Queries.Product;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ReadProductDTO>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<GetProductByIdQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReadProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando produto com ID: {Id}", request.Id);

        var product = await _productRepository.GetProductByIdAsync(request.Id);

        if (product == null)
        {
            throw new Exception("Produto não encontrado. Verifique o ID informado.");
        }

        return _mapper.Map<ReadProductDTO>(product);
    }
}
