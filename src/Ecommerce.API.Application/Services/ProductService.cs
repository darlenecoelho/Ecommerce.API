using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;

namespace Ecommerce.API.Application.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;

        public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        public async Task<List<ReadProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<ReadProductDTO>>(products);
        }

        public async Task<ReadProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Produto não encontrado. Verifique o id informado");
            }

            return _mapper.Map<ReadProductDTO>(product);
        }

        public async Task<ReadProductDTO> CreateProductAsync(CreateProductDTO product)
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(product.Name);
            if (existingProduct != null)
            {
                throw new Exception("O produto já foi cadastrado. Informe um nome diferente.");
            }

            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null || !category.Status)
            {
                throw new Exception("Não é possível cadastrar um produto em uma categoria inativa.");
            }

            var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(product.SubcategoryId);
            if (subcategory == null || !subcategory.Status)
            {
                throw new Exception("Não é possível cadastrar um produto em uma subcategoria inativa.");
            }

            var newProduct = _mapper.Map<Product>(product);
            var createdProduct = await _productRepository.AddProductAsync(newProduct);
            return _mapper.Map<ReadProductDTO>(createdProduct);
        }

        public async Task<ReadProductDTO> UpdateProductAsync(UpdateProductDTO product)
        {
            var createdProduct = await CreateProductAsync(new CreateProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                SubcategoryId = product.SubcategoryId
            });

            if (createdProduct.Id != product.Id)
            {
                throw new Exception("O ID do produto não corresponde ao ID fornecido.");
            }

            return createdProduct;
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
