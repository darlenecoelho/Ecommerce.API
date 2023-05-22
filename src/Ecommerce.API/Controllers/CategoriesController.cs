using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Services.Interfaces;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategoriesAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
    {
        var createdCategory = await _categoryService.CreateCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategoryAsync(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        await _categoryService.DeleteCategoryAsync(category);
        return NoContent();
    }
}
