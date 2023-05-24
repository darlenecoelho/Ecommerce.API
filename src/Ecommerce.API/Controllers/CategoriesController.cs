using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadCategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ReadCategoryDTO>> CreateCategory([FromBody] CreateCategoryDTO category)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadCategoryDTO>> UpdateCategory(int id, [FromBody] UpdateCategoryDTO category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("O ID da categoria na URL deve ser o mesmo que o ID fornecido no corpo da requisição.");
                }

                var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
