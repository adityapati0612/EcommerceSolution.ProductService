using Ecommerce.BuisnessLogicLayer.Mapper;
using Ecommerce.BuisnessLogicLayer.ServiceContracts;
using Ecommerce.BuisnessLogicLayer.Services;
using Ecommerce.BuisnessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSolution.ProductService.BuisnessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBuisnessLogicLayer(this IServiceCollection services)
    {
        //To Do: Add Buisness Logic layer services into the IoC container
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
   
        services.AddScoped<IProductService, ProductServices>(); // Ensure ProductService is a class, not a namespace
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidators>();
      //  services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestValidator>();
        return services;
    }
}
