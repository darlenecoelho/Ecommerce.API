using AutoMapper;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Lista todos os produtos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReadProductDTO>), 200)]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtém um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadProductDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception)
            {
                return NotFound("Produto não encontrado");
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">Dados do produto a ser criado.</param>
        [HttpPost]
        [ProducesResponseType(typeof(ReadProductDTO), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateProductAsync(CreateProductDTO product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProductByIdAsync), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="product">Dados do produto atualizado.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReadProductDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductDTO product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("O ID do produto na URL deve corresponder ao ID fornecido no corpo da requisição.");
                }

                var updatedProduct = await _productService.UpdateProductAsync(product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser excluído.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
