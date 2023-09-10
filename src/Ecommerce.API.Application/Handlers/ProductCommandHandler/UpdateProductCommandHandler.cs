using AutoMapper;
using Ecommerce.API.Application.Commands.Product;
using Ecommerce.API.Application.Responses.Product;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.ProductCommandHandler;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<UpdateProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(request.Id);

            if (existingProduct == null)
            {
                _logger.LogError("Produto não encontrado. ID: {productId}", request.Id);
                return new UpdateProductResponse
                {
                    Message = "Produto não encontrado."
                };
            }

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.Stock = request.Stock;
            existingProduct.CategoryId = request.CategoryId;
            existingProduct.SubcategoryId = request.SubcategoryId;
            existingProduct.LastUpdate = DateTime.UtcNow;

            await _productRepository.UpdateProductAsync(existingProduct);

            _logger.LogInformation($"Produto '{existingProduct.Name}' atualizado com sucesso.");

            return new UpdateProductResponse
            {
                Message = $"Produto '{existingProduct.Name}' atualizado com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao atualizar o produto: {ex.Message}");
            return new UpdateProductResponse
            {
                Message = "Ocorreu um erro ao atualizar o produto."
            };
        }
    }
}
