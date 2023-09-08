﻿using Ecommerce.API.Application.Commands.Product;
using Ecommerce.API.Application.Queries.Subcategory;
using Ecommerce.API.Application.Responses.Product;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.ProductCommandHandler;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateProductCommandHandler(IProductRepository productRepository, ILogger<CreateProductCommandHandler> logger, IMediator mediator)
    {
        _productRepository = productRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(request.Name);
            if (existingProduct != null)
            {
                _logger.LogError("O produto já foi cadastrado. Informe um nome diferente. Product Name: {productName}", request.Name);
                return new CreateProductResponse
                {
                    Message = "O produto já foi cadastrado. Informe um nome diferente."
                };
            }

            var category = await _mediator.Send(new GetSubcategoryByIdQuery { Id = request.CategoryId });
            if (category == null || !category.Status)
            {
                _logger.LogError("Não é possível cadastrar um produto em uma categoria inativa. Category ID: {categoryId}", request.CategoryId);
                return new CreateProductResponse
                {
                    Message = "Não é possível cadastrar um produto em uma categoria inativa."
                };
            }

            var subcategory = await _mediator.Send(new GetSubcategoryByIdQuery { Id = request.SubcategoryId });
            if (subcategory == null || !subcategory.Status)
            {
                _logger.LogError("Não é possível cadastrar um produto em uma subcategoria inativa. Subcategory ID: {subcategoryId}", request.SubcategoryId);
                return new CreateProductResponse
                {
                    Message = "Não é possível cadastrar um produto em uma subcategoria inativa."
                };
            }

            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                Status = request.Status,
                DateRegister = request.DateRegister,
                LastUpdate = DateTime.UtcNow
            };

            await _productRepository.AddProductAsync(newProduct);

            _logger.LogInformation($"Produto '{newProduct.Name}' criado com sucesso.");

            return new CreateProductResponse
            {
                ProductId = newProduct.Id,
                Message = $"Produto '{newProduct.Name}' criado com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao criar o produto: {ex.Message}");

            return new CreateProductResponse
            {
                Message = "Ocorreu um erro ao criar o produto."
            };
        }
    }
}
