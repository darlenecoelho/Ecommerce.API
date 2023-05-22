using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Domain.Services.Interfaces;

namespace Ecommerce.API.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetCategoryByIdAsync(id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        return await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        return await _categoryRepository.UpdateCategoryAsync(category);
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        await _categoryRepository.DeleteCategoryAsync(category);
    }
}
