using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Domain.Services.Interfaces;

namespace Ecommerce.API.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        return await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await _productRepository.DeleteProductAsync(product);
    }
}
