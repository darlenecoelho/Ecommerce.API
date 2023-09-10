using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Queries.Subcategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/subcategories")]
    public class SubcategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubcategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todas as subcategorias.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReadSubcategoryDTO>), 200)]
        public async Task<ActionResult<List<ReadSubcategoryDTO>>> GetAllSubcategories()
        {
            try
            {
                var query = new GetAllSubcategoriesQuery();
                var subcategories = await _mediator.Send(query);

                return Ok(subcategories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao listar subcategorias.");
            }
        }

        /// <summary>
        /// Obtém uma subcategoria pelo seu ID.
        /// </summary>
        /// <param name="id">ID da subcategoria.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReadSubcategoryDTO>> GetSubcategoryById(int id)
        {
            var subcategory = await _mediator.Send(new GetSubcategoryByIdQuery { Id = id });

            if (subcategory == null)
            {
                return NotFound();
            }

            return Ok(subcategory);
        }

        /// <summary>
        /// Cria uma nova subcategoria.
        /// </summary>
        /// <param name="command">Dados da subcategoria a ser criada.</param>
        [HttpPost]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 201)]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateSubcategoryAsync(CreateSubcategoryCommand command)
        {
            var response = await _mediator.Send(command);

            if (!string.IsNullOrEmpty(response.Message))
            {
                return BadRequest(new { message = response.Message });
            }

            return Ok();
        }

        /// <summary>
        /// Atualiza uma subcategoria existente.
        /// </summary>
        /// <param name="id">ID da subcategoria a ser atualizada.</param>
        /// <param name="command">Dados da subcategoria atualizada.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateSubcategory(int id, [FromBody] UpdateSubcategoryCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest("O ID informado na URL não corresponde ao ID no corpo da solicitação.");
                }

                var response = await _mediator.Send(command);

                if (response == null)
                {
                    return NotFound("Subcategoria não encontrada. Verifique o ID informado.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui uma subcategoria pelo seu ID.
        /// </summary>
        /// <param name="id">ID da subcategoria a ser excluída.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> DeleteSubcategoryAsync(int id)
        {
            try
            {
            //    await _subcategoryService.DeleteSubcategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
