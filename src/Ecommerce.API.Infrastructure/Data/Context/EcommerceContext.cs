using Ecommerce.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Ecommerce.API.Infrastructure.Data.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> dbOptions) : base(dbOptions)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
