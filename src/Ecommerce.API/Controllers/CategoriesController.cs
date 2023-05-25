using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Recupera todas as categorias.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ReadCategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Recupera uma categoria pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da categoria.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDTO>> GetCategoryById(int id)
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
        public async Task<ActionResult<ReadCategoryDTO>> CreateCategory([FromBody] CreateCategoryDTO category)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (InvalidOperationException ex)
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
        public async Task<ActionResult<ReadCategoryDTO>> UpdateCategory(int id, [FromBody] UpdateCategoryDTO category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("O ID da categoria na URL deve corresponder ao ID fornecido no corpo da requisição.");
                }

                var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                return Ok(updatedCategory);
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Categoria não encontrada.");
            }
        }
    }
}
