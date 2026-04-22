using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDBContext _dbContext;

    public ProductRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Product>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Product WHERE IsDeleted = 0";
        var products = await _dbContext.Connection.QueryAsync<Product>(sql);
        return products.ToList();
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _dbContext.Connection.GetAsync<Product>(id);
        if (product == null)
        {
            return null;
        }

        return product.IsDeleted == true ? null : product;
    }

    public async Task<Product> SaveAsync(Product product)
    {
        product.Id = await _dbContext.Connection.InsertAsync(product);
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        await _dbContext.Connection.UpdateAsync(product);
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await GetProductById(id);
        if (product == null)
        {
            return false;
        }

        product.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(product);
    }
}