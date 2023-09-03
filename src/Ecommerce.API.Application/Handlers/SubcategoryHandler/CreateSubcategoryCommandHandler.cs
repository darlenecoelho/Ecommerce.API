using AutoMapper;
using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Application.Handlers.SubcategoryHandler;

public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, ReadSubcategoryDTO>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSubcategoryCommandHandler> _logger;

    public CreateSubcategoryCommandHandler(
        ISubcategoryRepository subcategoryRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ILogger<CreateSubcategoryCommandHandler> logger)
    {
        _subcategoryRepository = subcategoryRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReadSubcategoryDTO> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Criando subcategoria: {Name}", request.Name);

        var existingSubcategory = await _subcategoryRepository.GetSubcategorytByNameAsync(request.Name);
        if (existingSubcategory != null)
        {
            var errorMessage = "Subcategoria já cadastrada. Por favor, altere o nome da subcategoria.";
            _logger.LogWarning(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }

        var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new Exception("Categoria não encontrada. Verifique o ID informado.");
        }

        if (!category.Status)
        {
            throw new InvalidOperationException("Não é possível cadastrar uma subcategoria em uma categoria inativa.");
        }

        var subcategoryToCreate = _mapper.Map<Subcategory>(request);

        var createdSubcategory = await _subcategoryRepository.AddSubcategoryAsync(subcategoryToCreate);

        var createdSubcategoryDTO = _mapper.Map<ReadSubcategoryDTO>(createdSubcategory);

        return createdSubcategoryDTO;
    }
}
