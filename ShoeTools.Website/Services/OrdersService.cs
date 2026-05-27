using Newtonsoft.Json;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Services;

public class OrdersService : IOrdersService
{
    private readonly string _baseUrl = "http://localhost:5263/";
    private readonly string _endpoint = "api/Orders";
    
    public async Task<Response<List<OrdersDto>>> GetAllOrders()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json  = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<OrdersDto>>>(json);
        return response;
    }

    public async Task<Response<OrdersDto>> GetOrderById(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client =  new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrdersDto>>(json);
        return response;
    }

    public async Task<Response<OrdersDto>> SaveAsync(OrdersDto orderDetailsDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(orderDetailsDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrdersDto>>(json);
        return response;
    }

    public async Task<Response<OrdersDto>> UpdateAsync(OrdersDto orderDetailsDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(orderDetailsDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<OrdersDto>>(json);
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