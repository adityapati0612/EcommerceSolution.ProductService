using Ecommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace Ecommerce.DataAccessLayer.RepositoryContracts;

/// <summary>
/// Represents a Repository to handle the 'Products' table
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Retrives all products asynchronously
    /// </summary>
    /// <returns>Returns all the product from table</returns>
    Task<IEnumerable<Product>> GetProductsAsync();  

    /// <summary>
    /// Retrieves all products information based on the specified condition asynchronously
    /// </summary>
    /// <param name="conditionExpression">The condition to filter products</param>
    /// <returns>Returning a collection of matching products</returns>
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product,bool>> conditionExpression);

    /// <summary>
    /// Retrives single product based on the condition
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns>Returns a single product or null</returns>
    Task<Product?> GetProductByCondition(Expression<Func<Product,bool>> conditionExpression);

    /// <summary>
    /// Adds a new product to product table
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Returns the product object if successfull or null</returns>
    Task<Product?> AddProduct(Product product); 

    /// <summary>
    /// update the existing product asynchronously
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Product> UpdateProduct(Product product);

    /// <summary>
    /// Delete the product asynchronously
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<bool> DeleteProduct(Guid productId);
}
