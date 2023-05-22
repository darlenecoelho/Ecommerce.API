using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> GetAllSubcategoriesAsync();
        Task<Subcategory> GetSubcategoryByIdAsync(int id);
        Task<Subcategory> AddSubcategoryAsync(Subcategory subcategory);
        Task<Subcategory> UpdateSubcategoryAsync(Subcategory subcategory);
        Task DeleteSubcategoryAsync(Subcategory subcategory);
    }
}
