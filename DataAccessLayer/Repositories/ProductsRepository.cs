using Ecommerce.DataAccessLayer.Entities;
using Ecommerce.DataAccessLayer.Context;
using Ecommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.DataAccessLayer.Repositories;

internal class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext dbContext;
    public ProductsRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Product?> AddProduct(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = await dbContext.Products.FirstOrDefaultAsync(x=>x.ProductId==productId);
        if (existingProduct == null) 
        { 
            return false;
        }
       dbContext.Products.Remove(existingProduct);
       await dbContext.SaveChangesAsync();
        
       return true;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await dbContext.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        Product? existingProduct=await dbContext.Products.FirstOrDefaultAsync(x=>x.ProductId==product.ProductId);
        if (existingProduct == null) 
        {
            return null;
        }
        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category= product.Category;
        await dbContext.SaveChangesAsync();

        return existingProduct;
    }
}
