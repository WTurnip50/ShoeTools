using ShoeTools.Core.Dto;

namespace ShoeTools.Api.Services.Interfaces;

public interface IProductDetailsService
{
    Task<bool> ProductExists(int detailsId);
    
    Task<ProductDetailsDto> SaveAsync(ProductDetailsDto detailsDto);
    
    Task<ProductDetailsDto> UpdateAsync(ProductDetailsDto details);
    
    Task<List<ProductDetailsDto>> GetAllProductDetails();
    
    Task<bool> DeleteAsync(int detailsId);
    
    Task<ProductDetailsDto> GetById(int detailsId);
}