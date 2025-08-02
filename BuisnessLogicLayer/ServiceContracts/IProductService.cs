using Ecommerce.DataAccessLayer.Entities;
using Ecommerce.BuisnessLogicLayer.DTO;
using System.Linq.Expressions;

namespace Ecommerce.BuisnessLogicLayer.ServiceContracts;

public interface IProductService
{
    /// <summary>
    /// Retrives the list of products from the product repository
    /// </summary>
    /// <returns>Returns list of ProductResponse objects</returns>
    Task<List<ProductResponse?>> GetProducts();

    /// <summary>
    /// Retrieves all products information based on the specified condition asynchronously
    /// </summary>
    /// <param name="conditionExpression">The condition to filter products</param>
    /// <returns>Returning a collection of matching products</returns>
    Task<IEnumerable<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Retrives single product based on the condition
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns>Returns a single product or null</returns>
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Adds a new product to product table
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Returns the product object if successfull or null</returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// update the existing product asynchronously
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);


    /// <summary>
    /// Delete the product asynchronously
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<bool> DeleteProduct(Guid productId);

}
