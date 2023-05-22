using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
