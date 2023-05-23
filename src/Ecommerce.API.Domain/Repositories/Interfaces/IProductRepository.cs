using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories.Interfaces;
public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
}
