using Ecommerce.API.Application.DTOs.Subcategory;

namespace Ecommerce.API.Application.Interfaces
{
    public interface ISubcategoryService
    {
        Task<List<ReadSubcategoryDTO>> GetAllSubcategoriesAsync();
        Task<ReadSubcategoryDTO> GetSubcategoryByIdAsync(int id);
        Task<ReadSubcategoryDTO> CreateSubcategoryAsync(CreateSubcategoryDTO subcategory);
        Task<ReadSubcategoryDTO> UpdateSubcategoryAsync(UpdateSubcategoryDTO subcategory);
        Task DeleteSubcategoryAsync(int id);
    }
}
