using AutoMapper;
using Ecommerce.BuisnessLogicLayer.DTO;
using Ecommerce.BuisnessLogicLayer.ServiceContracts;
using Ecommerce.DataAccessLayer.Entities;
using Ecommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Net;

namespace Ecommerce.BuisnessLogicLayer.Services;

internal class ProductServices : IProductService
{
    private readonly IValidator<ProductAddRequest> productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> productUpdateRequestValidator;
    private readonly IMapper mapper;
    private readonly IProductsRepository productsRepository;
    public ProductServices(IValidator<ProductAddRequest> productAddRequestValidator, 
        IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper,IProductsRepository productsRepository)
    {
        this.productAddRequestValidator = productAddRequestValidator;
        this.productUpdateRequestValidator = productUpdateRequestValidator;
        this.mapper = mapper;
        this.productsRepository = productsRepository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        { 
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        //Validate productAddRequest from FluentValidation
        ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

        //check validation results
        if (!validationResult.IsValid) 
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        //Attempt to add Product
        Product product=mapper.Map<Product>(productAddRequest);
        Product? addedProduct=await productsRepository.AddProduct(product);

        ProductResponse addedProductResponse=mapper.Map<ProductResponse>(addedProduct);
        return addedProductResponse;

    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = await productsRepository.GetProductByCondition(temp=>temp.ProductId==productId);

        if(existingProduct == null)
        {
            return false;
        }

        bool isDeleted = await productsRepository.DeleteProduct(productId);

        return isDeleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? getProduct=await productsRepository.GetProductByCondition(conditionExpression);

        if (getProduct == null)
        {
            return null;
        }
        ProductResponse getResponse=mapper.Map<ProductResponse>(getProduct);

        return getResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products = await productsRepository.GetProductsAsync();

        if(products == null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        IEnumerable<ProductResponse?> productsResponse =mapper.Map<IEnumerable<ProductResponse>>(products);

        return productsResponse.ToList();
    }

    public async Task<IEnumerable<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {

        IEnumerable<Product?> products = await productsRepository.GetProductsByCondition(conditionExpression);

        if (products == null)
        {
            return null;
        }
        
        IEnumerable<ProductResponse?> getResponse = mapper.Map<IEnumerable<ProductResponse>>(products);

        return getResponse;
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct = await productsRepository.GetProductByCondition(temp=>temp.ProductId==productUpdateRequest.ProductID);

        if (existingProduct == null)
        {
            throw new ArgumentException("Product Id is invalid");
        }
        //Validate productAddRequest from FluentValidation
        ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        //check validation results
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        //Attempt to add Product
        Product product = mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct = await productsRepository.UpdateProduct(product);

        ProductResponse updatedProductResponse = mapper.Map<ProductResponse>(updatedProduct);
        return updatedProductResponse;
    }
}
