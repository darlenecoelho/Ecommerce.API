using Ecommerce.API.Application.DTOs.Product;

namespace Ecommerce.API.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ReadProductDTO>> GetAllProductsAsync();
        Task<ReadProductDTO> GetProductByIdAsync(int id);
        Task<ReadProductDTO> CreateProductAsync(CreateProductDTO product);
        Task<ReadProductDTO> UpdateProductAsync(UpdateProductDTO product);
        Task DeleteProductAsync(int id);
    }
}
