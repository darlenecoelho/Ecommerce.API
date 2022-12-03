using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(EcommerceContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int? id)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.Subcategory)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Product>> FindByNameAsync(string name)
        {
            return await _context.Products.Where(product => product.Name.Contains(name)).OrderBy(product => product.Name).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
