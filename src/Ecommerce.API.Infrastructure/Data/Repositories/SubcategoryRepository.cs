using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly EcommerceContext _context;

        public SubcategoryRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<List<Subcategory>> GetAllSubcategoriesAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<Subcategory> GetSubcategoryByIdAsync(int id)
        {
            return await _context.Subcategories.FindAsync(id);
        }

        public async Task<Subcategory> AddSubcategoryAsync(Subcategory subcategory)
        {
            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task<Subcategory> UpdateSubcategoryAsync(Subcategory subcategory)
        {
            _context.Subcategories.Update(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task DeleteSubcategoryAsync(Subcategory subcategory)
        {
            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
        }
    }
}
