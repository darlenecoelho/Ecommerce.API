using Ecommerce.API.Application.Commands.Category;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Queries.Category;
using Ecommerce.API.Application.Responses.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controlador para operações relacionadas a categorias.
/// </summary>
[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista todas as categorias.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReadCategoryDTO>), 200)]
    public async Task<ActionResult<List<ReadCategoryDTO>>> GetAllCategoriesAsync()
    {
        var query = new GetAllCategoriesQuery();
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }

    /// <summary>
    /// Obtém uma categoria pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da categoria.</param>
    [HttpGet("{id}", Name = nameof(GetCategoryById))]
    [ProducesResponseType(typeof(ReadCategoryDTO), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<ReadCategoryDTO>> GetCategoryById(int id)
    {
        try
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id = id });

            return Ok(category);
        }
        catch (Exception)
        {
            return NotFound("Categoria não encontrada.");
        }
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    /// <param name="command">Os dados da categoria a ser criada.</param>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCategoryResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
    {
        var response = await _mediator.Send(command);

        if (!string.IsNullOrEmpty(response.Message))
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Atualiza uma categoria.
    /// </summary>
    /// <param name="command">O ID da categoria a ser atualizada.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReadCategoryDTO), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
    {
        try
        {
            command.Id = id;
            var response = await _mediator.Send(command);

            if (response == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno ao atualizar a categoria.");
        }
    }

    /// <summary>
    /// Exclui uma categoria pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da categoria a ser excluída.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return NoContent();
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno ao excluir a categoria.");
        }
    }
}
