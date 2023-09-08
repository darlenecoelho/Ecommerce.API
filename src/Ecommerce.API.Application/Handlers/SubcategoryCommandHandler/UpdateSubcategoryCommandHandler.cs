using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.Responses.Subcategory;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.SubcategoryCommandHandler;

public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, UpdateSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ILogger<UpdateSubcategoryCommandHandler> _logger;

    public UpdateSubcategoryCommandHandler(ISubcategoryRepository subcategoryRepository, ILogger<UpdateSubcategoryCommandHandler> logger)
    {
        _subcategoryRepository = subcategoryRepository;
        _logger = logger;
    }

    public async Task<UpdateSubcategoryResponse> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingSubcategory = await _subcategoryRepository.GetSubcategoryByIdAsync(request.Id);

            if (existingSubcategory == null)
            {
                _logger.LogWarning("Subcategoria com ID '{Id}' não encontrada.", request.Id);
                return new UpdateSubcategoryResponse
                {
                    Message = "Subcategoria não encontrada. Verifique o ID informado."
                };
            }

            if (!existingSubcategory.Status)
            {
                _logger.LogWarning("Não é possível atualizar uma subcategoria em uma categoria inativa.");
                return new UpdateSubcategoryResponse
                {
                    Message = "Não é possível atualizar uma subcategoria em uma categoria inativa."
                };
            }

            existingSubcategory.Name = request.Name;
            existingSubcategory.Status = request.Status;
            existingSubcategory.CategoryId = request.CategoryId;
            existingSubcategory.LastUpdate = DateTime.Now;

            await _subcategoryRepository.UpdateSubcategoryAsync(existingSubcategory);

            _logger.LogInformation($"Subcategoria '{existingSubcategory.Name}' atualizada com sucesso.");
            return new UpdateSubcategoryResponse
            {
                Message = $"Subcategoria '{existingSubcategory.Name}' atualizada com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao atualizar a subcategoria: {ex.Message}");
            return new UpdateSubcategoryResponse
            {
                Message = "Ocorreu um erro ao atualizar a subcategoria."
            };
        }
    }
}
