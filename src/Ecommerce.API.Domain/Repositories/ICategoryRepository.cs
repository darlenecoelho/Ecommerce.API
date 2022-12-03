using Ecommerce.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int? id);
        Task<List<Category>> GetByNameAsync(string name);
        void Update(Category category);
        void Remove(Category category);
    }
}
