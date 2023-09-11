using Ecommerce.API.Application.Commands.Category;
using Ecommerce.API.Application.Responses.Category;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.CategoryCommandHandler;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<CreateCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newCategory = new Category
            {
                Name = request.Name,
                Status = request.Status,
                DateRegister = request.DateRegister
            };

            await _categoryRepository.AddCategoryAsync(newCategory);
            _logger.LogInformation($"Categoria '{newCategory.Name}' criada com sucesso.");
            return new CreateCategoryResponse
            {
                Message = $"Categoria '{newCategory.Name}' criada com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao criar a categoria: {ex.Message}");
            return new CreateCategoryResponse
            {
                Message = "Ocorreu um erro ao criar a categoria."
            };
        }
    }
}


