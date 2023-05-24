using Ecommerce.API.Infrastructure.Data.Context;

namespace Ecommerce.API.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly EcommerceContext _context;
        protected BaseRepository(EcommerceContext context)
        {
            _context = context;
        }
    }
}
