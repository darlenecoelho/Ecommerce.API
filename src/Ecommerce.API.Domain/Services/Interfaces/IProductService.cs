using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
