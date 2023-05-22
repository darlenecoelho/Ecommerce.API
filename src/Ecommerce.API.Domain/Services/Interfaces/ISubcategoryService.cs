using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Domain.Services.Interfaces;
public interface ISubcategoryService
{
    Task<List<Subcategory>> GetAllSubcategoriesAsync();
    Task<Subcategory> GetSubcategoryByIdAsync(int id);
    Task<Subcategory> CreateSubcategoryAsync(Subcategory subcategory);
    Task<Subcategory> UpdateSubcategoryAsync(Subcategory subcategory);
    Task DeleteSubcategoryAsync(Subcategory subcategory);
}
