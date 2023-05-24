using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Application.Mappings;
using Ecommerce.API.Application.Services;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Infrastructure.Data.Context;
using Ecommerce.API.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Ecommerce.API.Infrastructure.IoC
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<EcommerceContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("EcommerceConnection")));

            // Add repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Add services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<IProductService, ProductService>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(CategoryMappingProfile), typeof(SubcategoryMappingProfile), typeof(ProductMappingProfile));
        }
    }
}
