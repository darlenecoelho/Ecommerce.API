using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IMapper _mapper;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<SubcategoryService> _logger;

        public SubcategoryService(ILogger<SubcategoryService> logger,
                                  IMapper mapper,
                                  ISubcategoryRepository subcategoryRepository, 
                                  ICategoryRepository categoryRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<ReadSubcategoryDTO>> GetAllSubcategoriesAsync()
        {
            _logger.LogInformation("Buscando todas as subcategorias");
            var subcategories = await _subcategoryRepository.GetAllSubcategoriesAsync();
            return _mapper.Map<List<ReadSubcategoryDTO>>(subcategories);
        }

        public async Task<ReadSubcategoryDTO> GetSubcategoryByIdAsync(int id)
        {
            _logger.LogInformation("Buscando subcategoria por ID: {Id}", id);
            var subcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(id);
            if (subcategory == null)
            {
                var errorMessage = "Subcategoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
            }

            return _mapper.Map<ReadSubcategoryDTO>(subcategory);
        }

        public async Task<ReadSubcategoryDTO> CreateSubcategoryAsync(CreateSubcategoryDTO subcategory)
        {
            _logger.LogInformation("Criando subcategoria: {Name}", subcategory.Name);
            var existingSubcategory = await _subcategoryRepository.GetSubcategorytByNameAsync(subcategory.Name);
            if (existingSubcategory != null)
            {
                var errorMessage = "Subcategoria já cadastrada. Por favor, altere o nome da subcategoria.";
                _logger.LogWarning(errorMessage);
                throw new InvalidOperationException(errorMessage);
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
            _logger.LogInformation("Atualizando subcategoria com ID: {Id}", subcategory.Id);
            var existingSubcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(subcategory.Id);
            if (existingSubcategory == null)
            {
                var errorMessage = "Subcategoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
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
            _logger.LogInformation("Deletando subcategoria com ID: {Id}", id);
            var existingSubcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(id);
            if (existingSubcategory == null)
            {
                var errorMessage = "Subcategoria não encontrada. Verifique o ID informado";
                _logger.LogWarning(errorMessage);
                throw new Exception(errorMessage);
            }

            await _subcategoryRepository.DeleteSubcategoryAsync(existingSubcategory);
        }
    }
}
