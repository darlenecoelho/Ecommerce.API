using Ecommerce.API.Application.Commands.Category;
using Ecommerce.API.Application.Responses.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.CategoryCommandHandler;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            if (existingCategory == null)
            {
                _logger.LogWarning("Categoria com ID {Id} não encontrada.", request.Id);
                return new UpdateCategoryResponse
                {
                    Message = "Categoria não encontrada. Verifique o ID informado."
                };
            }

            existingCategory.Name = request.Name;
            existingCategory.Status = request.Status;
            existingCategory.LastUpdate = DateTime.Now;

            await _categoryRepository.UpdateCategoryAsync(existingCategory);
            _logger.LogInformation($"Categoria com ID {existingCategory.Id} atualizada com sucesso.");
            return new UpdateCategoryResponse
            {
                Message = $"Categoria com ID {existingCategory.Id} atualizada com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao atualizar a categoria com ID {request.Id}: {ex.Message}");
            return new UpdateCategoryResponse
            {
                Message = "Ocorreu um erro ao atualizar a categoria."
            };
        }
    }
}
