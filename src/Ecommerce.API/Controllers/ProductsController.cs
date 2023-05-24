using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ReadProductDTO>> CreateProduct([FromBody] CreateProductDTO product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadProductDTO>> UpdateProduct(int id, [FromBody] UpdateProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest("The product ID provided does not match the updated product ID.");
            }

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
