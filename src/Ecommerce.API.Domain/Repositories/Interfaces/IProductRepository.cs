using Ecommerce.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.API.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
