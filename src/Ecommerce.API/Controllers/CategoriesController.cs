using AutoMapper;
using Ecommerce.API.Application.Commands.Category;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Application.Queries.Category;
using Ecommerce.API.Application.Responses.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;

        public CategoryController(IMapper mapper, IMediator mediator, ICategoryService categoryService)
        {
            _mediator = mediator;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
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
        [HttpGet("{id}", Name = nameof(GetCategoryByIdAsync))]
        [ProducesResponseType(typeof(ReadCategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReadCategoryDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
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
        /// <param name="request">Os dados da categoria a ser criada.</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand request)
        {
            var response = await _mediator.Send(request);

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
    /// <param name="id">O ID da categoria a ser atualizada.</param>
    /// <param name="category">Os dados atualizados da categoria.</param>
    [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReadCategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReadCategoryDTO>> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryDTO category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("O ID da categoria na URL deve corresponder ao ID fornecido no corpo da requisição.");
                }

                var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                return Ok("Categoria atualizada com sucesso.");
            }
            catch (Exception)
            {
                return NotFound("Categoria não encontrada.");
            }
        }

        /// <summary>
        /// Exclui uma categoria pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da categoria a ser excluída.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok("Categoria deletada com sucesso.");
            }
            catch (Exception)
            {
                return NotFound("Categoria não encontrada.");
            }
        }
    }
}
