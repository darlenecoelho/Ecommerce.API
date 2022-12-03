using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int? id);
        Task<List<Product>> FindByNameAsync(string name);
        void Update(Product product);
        void Remove(Product product);
    }
}
