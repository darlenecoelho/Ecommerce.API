using AutoMapper;
using Ecommerce.API.Application.Commands.Product;
using Ecommerce.API.Application.Responses.Product;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.ProductCommandHandler;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(
        IProductRepository productRepository,
        ILogger<DeleteProductCommandHandler> logger,
        IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteProductResponse();

        try
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                _logger.LogError("Produto não encontrado. ID: {productId}", request.Id);
                response.Message = "Produto não encontrado.";
                return response;
            }

            await _productRepository.DeleteProductAsync(product);

            _logger.LogInformation($"Produto ID {request.Id} excluído com sucesso.");

            response.Message = "Produto excluído com sucesso.";
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao excluir o produto: {ex.Message}");
            response.Message = "Ocorreu um erro ao excluir o produto.";
            return response;
        }
    }
}
