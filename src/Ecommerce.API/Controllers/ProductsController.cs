using Ecommerce.API.Application.Commands.Product;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Application.Queries.Product;
using Ecommerce.API.Application.Responses.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos os produtos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReadProductDTO>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var products = await _mediator.Send(query);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadProductDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<ReadProductDTO>> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="command">Dados do produto a ser criado.</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                if (!string.IsNullOrEmpty(response.Message))
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Erro ao criar um produto.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="command">Dados do produto atualizado.</param>
        /// <returns>O produto atualizado ou uma mensagem de erro.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateProductResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
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
                    return NotFound("Produto não encontrado. Verifique o ID informado.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser excluído.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
                var response = await _mediator.Send(command);

                if (response == null)
                {
                    return NotFound("Produto não encontrado. Verifique o ID informado.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}
