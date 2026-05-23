using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IProductDetailsService
{
    
    Task<Response<ProductDetailsDto>> SaveAsync(ProductDetailsDto detailsDto);
    
    Task<Response<ProductDetailsDto>> UpdateAsync(ProductDetailsDto details);
    
    Task<Response<List<ProductDetailsDto>>> GetAllProductDetails();
    
    Task<Response<bool>> DeleteAsync(int detailsId);
    
    Task<Response<ProductDetailsDto>> GetById(int detailsId);
}