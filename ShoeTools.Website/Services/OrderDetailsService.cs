using Newtonsoft.Json;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly string _baseUrl = "http://localhost:5263/";
    private readonly string _endpoint = "api/OrderDetails";
    public async Task<Response<List<OrderDetailsDto>>> GetAllDetails()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json  = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<OrderDetailsDto>>>(json);
        return response;
    }

    public async Task<Response<OrderDetailsDto>> GetDetailsById(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client =  new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrderDetailsDto>>(json);
        return response;
    }

    public async Task<Response<OrderDetailsDto>> SaveAsync(OrderDetailsDto orderDetailsDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(orderDetailsDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrderDetailsDto>>(json);
        return response;
    }

    public async Task<Response<OrderDetailsDto>> UpdateAsync(OrderDetailsDto orderDetailsDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(orderDetailsDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrderDetailsDto>>(json);
        return response;
    }

    public async Task<Response<bool>> Delete(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }
}