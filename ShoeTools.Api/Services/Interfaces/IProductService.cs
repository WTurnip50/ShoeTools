using ShoeTools.Core.Dto;

namespace ShoeTools.Api.Services.Interfaces;

public interface IProductService
{
    Task<bool> ProductExists(int productId);
    
    Task<ProductDto> SaveAsync(ProductDto productDto);
    
    Task<ProductDto> UpdateAsync(ProductDto productDto);
    
    Task<List<ProductDto>> GetAllProducts();
    
    Task<bool> DeleteAsync(int productId);
    
    Task<ProductDto> GetById(int productId);
}