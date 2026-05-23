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
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(detailsDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<ProductDetailsDto>>(json);
        return response;
    }

    public async Task<Response<ProductDetailsDto>> UpdateAsync(ProductDetailsDto details)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(details);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<ProductDetailsDto>>(json);
        return response;
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

    public async Task<Response<bool>> DeleteAsync(int detailsId)
    {
        var url = $"{_baseUrl}{_endpoint}/{detailsId}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }

    public async Task<Response<ProductDetailsDto>> GetById(int detailsId)
    {
        var url = $"{_baseUrl}{_endpoint}/{detailsId}";
        var client =  new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<ProductDetailsDto>>(json);
        return response;
    }
}