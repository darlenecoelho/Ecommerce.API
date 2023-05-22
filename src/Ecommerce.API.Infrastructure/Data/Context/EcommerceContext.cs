using Ecommerce.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;

namespace Ecommerce.API.Infrastructure.Data.Context;

public class EcommerceContext : DbContext
{
    public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
