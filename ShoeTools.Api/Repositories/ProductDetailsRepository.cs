using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories;

public class ProductDetailsRepository : IProductDetailsRepository
{
    private readonly IDBContext _dbContext;

    public ProductDetailsRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<ProductDetails>> GetAllAsync()
    {
        const string sql = "SELECT * FROM ProductDetails WHERE IsDeleted = 0";
        var products = await _dbContext.Connection.QueryAsync<ProductDetails>(sql);
        return products.ToList();
    }

    public async Task<ProductDetails> GetProductById(int id)
    {
        var product = await _dbContext.Connection.GetAsync<ProductDetails>(id);
        if (product == null)
        {
            return null;
        }

        return product.IsDeleted == true ? null : product;
    }

    public async Task<ProductDetails> SaveAsync(ProductDetails product)
    {
        product.Id = await _dbContext.Connection.InsertAsync(product);
        return product;
    }

    public async Task<ProductDetails> UpdateAsync(ProductDetails product)
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