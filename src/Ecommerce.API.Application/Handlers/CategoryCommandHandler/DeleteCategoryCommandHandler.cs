using Ecommerce.API.Application.Commands.Category;
using Ecommerce.API.Application.Responses.Category;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.CategoryCommandHandler;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<DeleteCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            if (category == null)
            {
                return new DeleteCategoryResponse
                {
                    Message = "Categoria não encontrada."
                };
            }

            await _categoryRepository.DeleteCategoryAsync(category);

            return new DeleteCategoryResponse
            {
                Message = "Categoria excluída com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao excluir a categoria: {ex.Message}");
            return new DeleteCategoryResponse
            {
                Message = "Ocorreu um erro ao excluir a categoria."
            };
        }
    }
}
