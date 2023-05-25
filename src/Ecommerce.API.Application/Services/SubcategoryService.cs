using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;

namespace Ecommerce.API.Application.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IMapper _mapper;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SubcategoryService(IMapper mapper, ISubcategoryRepository subcategoryRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<ReadSubcategoryDTO>> GetAllSubcategoriesAsync()
        {
            var subcategories = await _subcategoryRepository.GetAllSubcategoriesAsync();
            return _mapper.Map<List<ReadSubcategoryDTO>>(subcategories);
        }

        public async Task<ReadSubcategoryDTO> GetSubcategoryByIdAsync(int id)
        {
            var subcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(id);
            return _mapper.Map<ReadSubcategoryDTO>(subcategory);
        }

        public async Task<ReadSubcategoryDTO> CreateSubcategoryAsync(CreateSubcategoryDTO subcategory)
        {
            var existingSubcategory = await _subcategoryRepository.GetSubcategorytByNameAsync(subcategory.Name);
            if (existingSubcategory != null)
            {
                throw new InvalidOperationException("Subcategoria já cadastrada. Por favor, altere o nome da subcategoria.");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(subcategory.CategoryId);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada. Verifique o ID informado.");
            }

            if (!category.Status)
            {
                throw new InvalidOperationException("Não é possível cadastrar uma subcategoria em uma categoria inativa.");
            }

            var newSubcategory = _mapper.Map<Subcategory>(subcategory);
            var createdSubcategory = await _subcategoryRepository.AddSubcategoryAsync(newSubcategory);

            return _mapper.Map<ReadSubcategoryDTO>(createdSubcategory);
        }

        public async Task<ReadSubcategoryDTO> UpdateSubcategoryAsync(UpdateSubcategoryDTO subcategory)
        {
            var existingSubcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(subcategory.Id);
            if (existingSubcategory == null)
            {
                throw new Exception("Subcategoria não encontrada. Verifique o ID informado.");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(subcategory.CategoryId);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada. Verifique o ID informado.");
            }

            if (!category.Status)
            {
                throw new InvalidOperationException("Não é possível cadastrar uma subcategoria em uma categoria inativa.");
            }

            var subcategoryToUpdate = _mapper.Map<Subcategory>(subcategory);
            subcategoryToUpdate.LastUpdate = DateTime.UtcNow;

            var updatedSubcategory = await _subcategoryRepository.UpdateSubcategoryAsync(subcategoryToUpdate);

            return _mapper.Map<ReadSubcategoryDTO>(updatedSubcategory);
        }

        public async Task DeleteSubcategoryAsync(int id)
        {
            var existingSubcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(id);
            if (existingSubcategory == null)
            {
                throw new Exception("Subcategoria não encontrada. Verifique o ID informado.");
            }

            await _subcategoryRepository.DeleteSubcategoryAsync(existingSubcategory);
        }
    }
}
