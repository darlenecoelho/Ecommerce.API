using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories
{
    public interface ISubcategoryRepository
    {
        Task<IEnumerable<Subcategory>> ListAsync();
        Task<Subcategory> FindByIdAsync(int? id);
        Task<List<Subcategory>> FindByNameAsync(string name);
        Task AddAsync(Subcategory subcategory);
        void Update(Subcategory subcategory);
        void Remove(Subcategory subcategory);

    }
}
