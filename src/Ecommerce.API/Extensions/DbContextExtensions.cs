using Ecommerce.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Extensions
{
    internal static class DbContextExtensions
    {
        internal static void AddEcommerceContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("EcommerceConnection");
            string serverVersion = "8.0.27";

            services.AddDbContext<EcommerceContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(serverVersion),
                    builder => builder.MigrationsAssembly("Ecommerce.API")));
        }
    }
}
