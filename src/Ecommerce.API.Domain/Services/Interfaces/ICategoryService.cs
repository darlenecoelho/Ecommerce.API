using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
