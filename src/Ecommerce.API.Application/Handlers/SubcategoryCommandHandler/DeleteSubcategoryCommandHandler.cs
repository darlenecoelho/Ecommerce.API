using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.Responses.Subcategory;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.SubcategoryCommandHandler;

public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, DeleteSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ILogger<DeleteSubcategoryCommandHandler> _logger;

    public DeleteSubcategoryCommandHandler(ISubcategoryRepository subcategoryRepository, ILogger<DeleteSubcategoryCommandHandler> logger)
    {
        _subcategoryRepository = subcategoryRepository;
        _logger = logger;
    }

    public async Task<DeleteSubcategoryResponse> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var subcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(request.Id);

            if (subcategory == null)
            {
                return new DeleteSubcategoryResponse
                {
                    Message = "Subcategoria não encontrada. Verifique o ID informado."
                };
            }

            await _subcategoryRepository.DeleteSubcategoryAsync(subcategory);

            _logger.LogInformation($"Subcategoria '{subcategory.Name}' excluída com sucesso.");
            return new DeleteSubcategoryResponse
            {
                Message = $"Subcategoria '{subcategory.Name}' excluída com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao excluir a subcategoria: {ex.Message}");
            return new DeleteSubcategoryResponse
            {
                Message = "Ocorreu um erro ao excluir a subcategoria."
            };
        }
    }
}
