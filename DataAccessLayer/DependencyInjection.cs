using Ecommerce.DataAccessLayer.Context;
using Ecommerce.DataAccessLayer.Repositories;
using Ecommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace EcommerceSolution.ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration config)
    {
        //To Do: Add data access layer services into the IoC container
        string connectionStringTemplate = config.GetConnectionString("DefaultConnection")!;
        string connectionString = connectionStringTemplate
          .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
          .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"))
          .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
          .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
          .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"));

        services.AddDbContext<ApplicationDbContext>(options => 
        { 
            options.UseMySQL(connectionString);
        });
        services.AddScoped<IProductsRepository,ProductsRepository>();
        return services;
    }
}
