using Ecommerce.BuisnessLogicLayer.DTO;
using Ecommerce.BuisnessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Ecommerce.ProductMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async ([FromServices] IProductService productService) =>
        {
           var products= await productService.GetProducts();
            return Results.Ok(products);
        });

        //GET /api/products/search/product-id
        app.MapGet("/api/products/search/product-id/{ProductID}", async (IProductService productService, Guid ProductID) =>
        {
            ProductResponse? product = await productService.GetProductByCondition(temp=>temp.ProductId==ProductID);
            return Results.Ok(product);
        });

        //GET /api/products/search/product-id
        app.MapGet("/api/products/search/{SearchString}", async ( IProductService productService, string SearchString) =>
        {
            IEnumerable<ProductResponse?> productByName = await productService.GetProductsByCondition(temp =>
            temp.ProductName != null &&
            temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));


            IEnumerable<ProductResponse?> productByCategory = await productService.GetProductsByCondition(temp =>
            temp.Category != null &&
            temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var response=productByName.Union(productByCategory);
            return Results.Ok(response);
        });


        //POST /api/products/
        app.MapPost("/api/products/", async (IProductService productService, IValidator<ProductAddRequest> productAddRequestValidator , ProductAddRequest productAddRequest) =>
        {
            ValidationResult result= await productAddRequestValidator.ValidateAsync(productAddRequest);
            if(!result.IsValid)
            {
                Dictionary<string, string[]> errors = result.Errors.GroupBy(temp => temp.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                
                return Results.ValidationProblem (errors);
            }

           ProductResponse? response= await productService.AddProduct(productAddRequest);

            if (response != null)
                return Results.Created($"/api/products/search/product-id/{response.ProductId}", response);
            else
                return Results.Problem("Error in adding product");
        });

        //Put /api/products/
        app.MapPut("/api/products/", async ( IProductService productService,  IValidator<ProductUpdateRequest> productUpdateRequestValidator, ProductUpdateRequest productUpdateRequest) =>
        {
            ValidationResult result = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!result.IsValid)
            {
                Dictionary<string, string[]> errors = result.Errors.GroupBy(temp => temp.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            ProductResponse? response = await productService.UpdateProduct(productUpdateRequest);

            if (response != null)
                return Results.Ok(response);
            else
                return Results.Problem("Error in updating product");
        });

        //Delete /api/products/
        app.MapDelete("/api/products/{ProductID:guid}", async ( IProductService productService,Guid ProductID) =>
        {
            

           bool isDelete = await productService.DeleteProduct(ProductID);

            if (isDelete)
                return Results.Ok(true);
            else
                return Results.Problem("Error in Deleting product");
        });
        return app;
    }
}
