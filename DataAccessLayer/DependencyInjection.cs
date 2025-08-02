using Ecommerce.DataAccessLayer.Context;
using Ecommerce.DataAccessLayer.Repositories;
using Ecommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSolution.ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration config)
    {
        //To Do: Add data access layer services into the IoC container
        services.AddDbContext<ApplicationDbContext>(options => 
        { 
            options.UseMySQL(config.GetConnectionString("DefaultConnection")!);
        });
        services.AddScoped<IProductsRepository,ProductsRepository>();
        return services;
    }
}
