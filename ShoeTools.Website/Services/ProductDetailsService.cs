using Newtonsoft.Json;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Services;

public class ProductDetailsService : IProductDetailsService
{
    private readonly string _baseUrl = "http://localhost:5263/";
    private readonly string _endpoint = "api/ProductDetails";

    public ProductDetailsService()
    {
    }

    public async Task<Response<ProductDetailsDto>> SaveAsync(ProductDetailsDto detailsDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ProductDetailsDto>> UpdateAsync(ProductDetailsDto details)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<ProductDetailsDto>>> GetAllProductDetails()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json  = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<ProductDetailsDto>>>(json);
        return response;
    }

    public async Task<bool> DeleteAsync(int detailsId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ProductDetailsDto>> GetById(int detailsId)
    {
        throw new NotImplementedException();
    }
}