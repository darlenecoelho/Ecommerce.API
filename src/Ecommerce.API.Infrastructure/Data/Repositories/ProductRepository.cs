using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly EcommerceContext _context;

    public ProductRepository(EcommerceContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);

    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    public async Task<Product> GetProductByNameAsync(string product)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == product);
    }
}
