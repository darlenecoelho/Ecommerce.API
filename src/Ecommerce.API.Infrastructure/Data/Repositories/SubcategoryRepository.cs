using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public class SubcategoryRepository : BaseRepository, ISubcategoryRepository
    {
        public SubcategoryRepository(EcommerceContext context) : base(context) { }

        public async Task<IEnumerable<Subcategory>> ListAsync()
        {
            return await _context.Subcategories
                                 .AsNoTracking()
                                 .ToListAsync();
        }
        public async Task<Subcategory> FindByIdAsync(int? id)
        {
            return await _context.Subcategories
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Subcategory>> FindByNameAsync(string name)
        {
            return await _context.Subcategories
                .Where(subcategory => subcategory.Name.Contains(name))
                .OrderBy(subcategory => subcategory.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Subcategory subcategory)
        {
            await _context.Subcategories.AddAsync(subcategory);
        }

        public void Update(Subcategory subcategory)
        {
            _context.Subcategories.Update(subcategory);
        }

        public void Remove(Subcategory subcategory)
        {
            _context.Subcategories.Remove(subcategory);
        }
    }
}
