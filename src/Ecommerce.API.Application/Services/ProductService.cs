using AutoMapper;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;

namespace Ecommerce.API.Application.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ReadProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<ReadProductDTO>>(products);
        }

        public async Task<ReadProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ReadProductDTO>(product);
        }

        public async Task<ReadProductDTO> CreateProductAsync(CreateProductDTO product)
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(product.Name);
            if (existingProduct != null)
            {
                throw new Exception("O produto já foi cadastrado. Por favor, informe um nome diferente.");
            }

            var newProduct = _mapper.Map<Product>(product);
            var createdProduct = await _productRepository.AddProductAsync(newProduct);
            return _mapper.Map<ReadProductDTO>(createdProduct);
        }

        public async Task<ReadProductDTO> UpdateProductAsync(UpdateProductDTO product)
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(product.Name);
            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                throw new Exception("O produto já foi cadastrado com esse nome. Por favor, informe um nome diferente.");
            }

            var updatedProduct = await _productRepository.GetProductByIdAsync(product.Id);
            if (updatedProduct == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            _mapper.Map(product, updatedProduct);
            updatedProduct.LastUpdate = DateTime.UtcNow;

            await _productRepository.UpdateProductAsync(updatedProduct);
            return _mapper.Map<ReadProductDTO>(updatedProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            await _productRepository.DeleteProductAsync(product);
        }
    }
}
