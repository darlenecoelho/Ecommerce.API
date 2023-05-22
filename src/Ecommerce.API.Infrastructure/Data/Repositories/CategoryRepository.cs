using Ecommerce.API.Domain.Entities;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceContext _context;

        public CategoryRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
