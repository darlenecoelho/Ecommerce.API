using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
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
            var categories = await _categoryService.GetAllCategoriesAsync();
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
        /// <param name="category">A categoria a ser criada.</param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO category)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(category);
                return Ok("Categoria cadastrada com sucesso.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Categoria já cadastrada. Por favor, altere o nome da categoria.");
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
