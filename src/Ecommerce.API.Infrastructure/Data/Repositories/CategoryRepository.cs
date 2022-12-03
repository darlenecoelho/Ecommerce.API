using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(EcommerceContext context) : base(context) { }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<List<Category>> FindByNameAsync(string name)
        {
            return await _context.Categories
                .Where(category => category.Name.Contains(name))
                .OrderBy(category => category.Name)
                .ToListAsync();
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
