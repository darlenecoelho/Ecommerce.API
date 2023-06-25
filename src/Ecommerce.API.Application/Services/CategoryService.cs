using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            ILogger<CategoryService> logger,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<ReadCategoryDTO>> GetAllCategoriesAsync()
        {
            _logger.LogInformation("Buscando todas as categorias");
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<ReadCategoryDTO>>(categories);
        }

        public async Task<ReadCategoryDTO> GetCategoryByIdAsync(int id)
        {
            _logger.LogInformation("Buscando categoria por ID: {Id}", id);
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                var errorMessage = "Categoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
            }

            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<ReadCategoryDTO> CreateCategoryAsync(CreateCategoryDTO category)
        {
            _logger.LogInformation("Criando categoria: {Name}", category.Name);
            var existingCategory = await _categoryRepository.GetCategoryByNameAsync(category.Name);

            if (existingCategory != null)
            {
                var errorMessage = "Categoria já cadastrada. Por favor, altere o nome da categoria.";
                _logger.LogWarning(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            var newCategory = _mapper.Map<Category>(category);
            var createdCategory = await _categoryRepository.AddCategoryAsync(newCategory);

            return _mapper.Map<ReadCategoryDTO>(createdCategory);
        }

        public async Task<ReadCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO category)
        {
            _logger.LogInformation("Atualizando categoria com ID: {Id}", category.Id);
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);

            if (existingCategory == null)
            {
                var errorMessage = "Categoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
            }

            existingCategory.Name = category.Name;
            existingCategory.LastUpdate = DateTime.Now;

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(existingCategory);

            return _mapper.Map<ReadCategoryDTO>(updatedCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _logger.LogInformation("Deletando categoria com ID: {Id}", id);
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
            {
                var errorMessage = "Categoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
            }

            await _categoryRepository.DeleteCategoryAsync(existingCategory);
        }
    }
}
