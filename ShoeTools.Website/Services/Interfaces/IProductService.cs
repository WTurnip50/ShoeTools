using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IProductService
{
    Task<Response<List<ProductDto>>> GetAllProducts();
    Task<Response<ProductDto>> GetProductById(int id);
    Task<Response<ProductDto>> SaveAsync(ProductDto productDto);
    Task<Response<ProductDto>> UpdateAsync(ProductDto productDto);
    Task<Response<bool>> Delete(int id);
}