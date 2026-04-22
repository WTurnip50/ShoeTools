using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetProductById(int id);
    Task <Product> SaveAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}