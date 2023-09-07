using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Handlers.CategoryCommandHandler;
using Ecommerce.API.Application.Handlers.SubcategoryHandler;
using Ecommerce.API.Application.Interfaces;
using Ecommerce.API.Application.Mappings;
using Ecommerce.API.Application.Queries.Subcategory;
using Ecommerce.API.Application.Services;
using Ecommerce.API.Domain.Repositories.Interfaces;
using Ecommerce.API.Infrastructure.Data.Context;
using Ecommerce.API.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.API.Infrastructure.IoC;
public static class ServiceConfiguration
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<EcommerceContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("EcommerceConnection")));
        services.AddLogging();

        // Add repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        // Add services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubcategoryService, SubcategoryService>();
        services.AddScoped<IProductService, ProductService>();

        // Add AutoMapper
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(CreateCategoryCommandHandler).Assembly);
        services.AddTransient<IRequestHandler<GetSubcategoryByIdQuery, ReadSubcategoryDTO>, GetSubcategoryByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllSubcategoriesQuery, List<ReadSubcategoryDTO>>, GetAllSubcategoriesQueryHandler>();
        services.AddScoped<IRequestHandler<CreateSubcategoryCommand, ReadSubcategoryDTO>, CreateSubcategoryCommandHandler>();
        services.AddAutoMapper(typeof(CategoryMappingProfile), typeof(SubcategoryMappingProfile), typeof(ProductMappingProfile));

    }
}
