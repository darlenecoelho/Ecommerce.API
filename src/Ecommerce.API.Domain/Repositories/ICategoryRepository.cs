using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int? id);
        Task<List<Category>> FindByNameAsync(string name);
        void Update(Category category);
        void Remove(Category category);
    }
}
