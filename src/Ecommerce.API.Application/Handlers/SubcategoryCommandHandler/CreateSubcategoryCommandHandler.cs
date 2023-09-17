using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.Responses.Subcategory;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.SubcategoryCommandHandler;

public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, CreateSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ILogger<CreateSubcategoryCommandHandler> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CreateSubcategoryCommandHandler(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, ILogger<CreateSubcategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _subcategoryRepository = subcategoryRepository;
        _logger = logger;
    }

    public async Task<CreateSubcategoryResponse> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            if (category == null || !category.Status)
            {
                _logger.LogWarning("Não é possível cadastrar uma subcategoria em uma categoria inativa.");
                return new CreateSubcategoryResponse
                {
                    Message = "Não é possível cadastrar uma subcategoria em uma categoria inativa."
                };
            }

            var newSubcategory = new Subcategory
            {
                Name = request.Name,
                Status = request.Status,
                CategoryId = request.CategoryId,
                DateRegister = DateTime.Now
        };

            await _subcategoryRepository.AddSubcategoryAsync(newSubcategory);
            _logger.LogInformation($"Subcategoria '{newSubcategory.Name}' criada com sucesso.");
            return new CreateSubcategoryResponse
            {
                Message = $"Subcategoria '{newSubcategory.Name}' criada com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao criar a subcategoria: {ex.Message}");
            return new CreateSubcategoryResponse
            {
                Message = "Ocorreu um erro ao criar a subcategoria."
            };
        }
    }
}

