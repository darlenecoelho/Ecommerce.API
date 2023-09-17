using AutoMapper;
using Ecommerce.API.Application.Commands.Product;
using Ecommerce.API.Application.Responses.Product;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.ProductCommandHandler;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<CreateProductCommandHandler> logger,
        ICategoryRepository categoryRepository,
        ISubcategoryRepository subcategoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _categoryRepository = categoryRepository;
        _subcategoryRepository = subcategoryRepository;
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
                    Message = "O produto já existe. Informe um nome diferente."
                };
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            if (category == null || !category.Status)
            {
                _logger.LogError("Não é possível cadastrar um produto em uma categoria inativa. Category ID: {categoryId}", request.CategoryId);
                return new CreateProductResponse
                {
                    Message = "Não é possível cadastrar um produto em uma categoria inativa."
                };
            }

            var subcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(request.SubcategoryId);
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
                DateRegister = DateTime.Now,
            LastUpdate = DateTime.UtcNow
            };

            var createdProduct = await _productRepository.AddProductAsync(newProduct);

            _logger.LogInformation($"Produto '{createdProduct.Name}' criado com sucesso.");

            return new CreateProductResponse
            {
                Message = "Produto criado com sucesso."
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
