using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Domain.Services.Interfaces;

namespace Ecommerce.API.Application.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<List<Subcategory>> GetAllSubcategoriesAsync()
        {
            return await _subcategoryRepository.GetAllSubcategoriesAsync();
        }

        public async Task<Subcategory> GetSubcategoryByIdAsync(int id)
        {
            return await _subcategoryRepository.GetSubcategoryByIdAsync(id);
        }

        public async Task<Subcategory> CreateSubcategoryAsync(Subcategory subcategory)
        {
            return await _subcategoryRepository.AddSubcategoryAsync(subcategory);
        }

        public async Task<Subcategory> UpdateSubcategoryAsync(Subcategory subcategory)
        {
            return await _subcategoryRepository.UpdateSubcategoryAsync(subcategory);
        }

        public async Task DeleteSubcategoryAsync(Subcategory subcategory)
        {
            await _subcategoryRepository.DeleteSubcategoryAsync(subcategory);
        }
    }
}
