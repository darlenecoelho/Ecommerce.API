using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;

namespace Ecommerce.API.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<ReadCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<ReadCategoryDTO>>(categories);
        }

        public async Task<ReadCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada. Verifique o id informado");
            }

            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<ReadCategoryDTO> CreateCategoryAsync(CreateCategoryDTO category)
        {
            if (await _categoryRepository.GetCategoryByNameAsync(category.Name) != null)
            {
                throw new InvalidOperationException("Categoria já cadastrada. Por favor, altere o nome da categoria.");
            }

            var newCategory = _mapper.Map<Category>(category);
            var createdCategory = await _categoryRepository.AddCategoryAsync(newCategory);

            return _mapper.Map<ReadCategoryDTO>(createdCategory);
        }

        public async Task<ReadCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO category)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new Exception("Categoria não encontrada. Verifique o id informado");
            }

            if (await _categoryRepository.GetCategoryByNameAsync(category.Name) != null && existingCategory.Name != category.Name)
            {
                throw new InvalidOperationException("Categoria já cadastrada. Por favor, altere o nome da categoria.");
            }

            existingCategory.Name = category.Name;
            existingCategory.LastUpdate = DateTime.UtcNow;

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(existingCategory);

            return _mapper.Map<ReadCategoryDTO>(updatedCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Categoria não encontrada. Verifique o id informado");
            }

            await _categoryRepository.DeleteCategoryAsync(existingCategory);
        }
    }
}
