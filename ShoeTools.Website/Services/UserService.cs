using Newtonsoft.Json;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Services;

public class UserService : IUserService
{
    private readonly string _baseUrl = "http://localhost:5263/";
    private readonly string _endpoint = "api/AppUsers";
    
    public UserService()
    {
    }

    public async Task<Response<List<AppUsersDto>>> GetAllUsers()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json  = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<AppUsersDto>>>(json);
        return response;
    }

    public async Task<Response<AppUsersDto>> GetUserById(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client =  new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<AppUsersDto>>(json);
        return response;
    }

    public async Task<Response<AppUsersDto>> SaveAsync(AppUsersDto usersDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(usersDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        var response = JsonConvert.DeserializeObject<Response<AppUsersDto>>(json);
        return response;
    }

    public async Task<Response<AppUsersDto>> UpdateAsync(AppUsersDto usersDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(usersDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<AppUsersDto>>(json);
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

    public async Task<Response<AppUsersDto>> LogIn(AppUsersDto user)
    {
        var url = $"{_baseUrl}{_endpoint}/login";
        var jsonRequest = JsonConvert.SerializeObject(user);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url,content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<AppUsersDto>>(json);
        return response;
    }
}