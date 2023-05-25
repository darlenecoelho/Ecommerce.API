using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/subcategories")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly IMapper _mapper;

        public SubcategoryController(ISubcategoryService subcategoryService, IMapper mapper)
        {
            _subcategoryService = subcategoryService ?? throw new ArgumentNullException(nameof(subcategoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Lista todas as subcategorias.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReadSubcategoryDTO>), 200)]
        public async Task<IActionResult> GetAllSubcategoriesAsync()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            return Ok(subcategories);
        }

        /// <summary>
        /// Obtém uma subcategoria pelo seu ID.
        /// </summary>
        /// <param name="id">ID da subcategoria.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetSubcategoryByIdAsync(int id)
        {
            try
            {
                var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);
                return Ok(subcategory);
            }
            catch (Exception)
            {
                return NotFound("Subcategoria não encontrada");
            }
        }

        /// <summary>
        /// Cria uma nova subcategoria.
        /// </summary>
        /// <param name="subcategory">Dados da subcategoria a ser criada.</param>
        [HttpPost]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateSubcategoryAsync(CreateSubcategoryDTO subcategory)
        {
            try
            {
                var createdSubcategory = await _subcategoryService.CreateSubcategoryAsync(subcategory);
                return CreatedAtAction(nameof(GetSubcategoryByIdAsync), new { id = createdSubcategory.Id }, createdSubcategory);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Subcategoria já cadastrada. Por favor, altere o nome da subcategoria.");
            }
        }

        /// <summary>
        /// Atualiza uma subcategoria existente.
        /// </summary>
        /// <param name="id">ID da subcategoria a ser atualizada.</param>
        /// <param name="subcategory">Dados da subcategoria atualizada.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReadSubcategoryDTO>> UpdateSubcategoryAsync(int id, [FromBody] UpdateSubcategoryDTO subcategory)
        {
            try
            {
                if (id != subcategory.Id)
                {
                    return BadRequest("O ID da subcategoria na URL deve corresponder ao ID fornecido no corpo da requisição.");
                }

                var updatedSubcategory = await _subcategoryService.UpdateSubcategoryAsync(subcategory);
                return Ok(updatedSubcategory);
            }
            catch (Exception)
            {
                return NotFound("Subcategoria não encontrada.");
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
                await _subcategoryService.DeleteSubcategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
