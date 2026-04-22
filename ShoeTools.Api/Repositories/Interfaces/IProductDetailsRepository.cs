using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public interface IProductDetailsRepository
{
    Task<List<ProductDetails>> GetAllAsync();
    Task<ProductDetails> GetProductById(int id);
    Task <ProductDetails> SaveAsync(ProductDetails product);
    Task<ProductDetails> UpdateAsync(ProductDetails product);
    Task<bool> DeleteAsync(int id);
}