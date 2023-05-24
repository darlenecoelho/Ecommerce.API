using Ecommerce.API.Application.DTOs.Category;

namespace Ecommerce.API.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<ReadCategoryDTO>> GetAllCategoriesAsync();
        Task<ReadCategoryDTO> GetCategoryByIdAsync(int id);
        Task<ReadCategoryDTO> CreateCategoryAsync(CreateCategoryDTO category);
        Task<ReadCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO category);
        Task DeleteCategoryAsync(int id);
    }
}
