using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/subcategories")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly IMapper _mapper;

        public SubcategoryController(ISubcategoryService subcategoryService, IMapper mapper)
        {
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadSubcategoryDTO>>> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            return Ok(subcategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSubcategoryDTO>> GetSubcategoryById(int id)
        {
            var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            return Ok(subcategory);
        }

        [HttpPost]
        public async Task<ActionResult<ReadSubcategoryDTO>> CreateSubcategory([FromBody] CreateSubcategoryDTO subcategory)
        {
            try
            {
                var createdSubcategory = await _subcategoryService.CreateSubcategoryAsync(subcategory);
                return CreatedAtAction(nameof(GetSubcategoryById), new { id = createdSubcategory.Id }, createdSubcategory);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadSubcategoryDTO>> UpdateSubcategory(int id, [FromBody] UpdateSubcategoryDTO subcategory)
        {
            if (id != subcategory.Id)
            {
                return BadRequest("O ID da subcategoria informado não corresponde ao ID da URL.");
            }

            try
            {
                var updatedSubcategory = await _subcategoryService.UpdateSubcategoryAsync(subcategory);
                return Ok(updatedSubcategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubcategory(int id)
        {
            try
            {
                await _subcategoryService.DeleteSubcategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
