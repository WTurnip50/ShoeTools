using System.Text;
using Newtonsoft.Json;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Services;

public class ClientService : IClientService
{
    private readonly string _baseUrl = "http://localhost:5263/";
    private readonly string _endpoint = "api/Client";
    
    
    public async Task<Response<List<ClientDto>>> GetAllClients()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json  = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<ClientDto>>>(json);
        return response;
    }

    public async Task<Response<ClientDto>> GetClientById(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client =  new HttpClient();
        var res = await client.GetAsync(url);
        if (!res.IsSuccessStatusCode)
        {
            return new Response<ClientDto> { Data = null };
        }
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<ClientDto>>(json);
        return response;
    }

    public async Task<Response<ClientDto>> SaveAsync(ClientDto clientDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(clientDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<ClientDto>>(json);
        return response;
    }

    public async Task<Response<ClientDto>> UpdateAsync(ClientDto clientDto)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(clientDto);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var res = await client.PutAsync(url, content);

        var rawResponse = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
        {
            // Aquí registramos el cuerpo tal cual lo devuelve el servidor
            Console.WriteLine($"Error HTTP {res.StatusCode}: {rawResponse}");
            throw new Exception($"Error en la actualización ({res.StatusCode}). Respuesta: {rawResponse}");
        }

        try
        {
            var response = JsonConvert.DeserializeObject<Response<ClientDto>>(rawResponse);
            return response;
        }
        catch (JsonReaderException ex)
        {
            // Capturamos el error de parsing y mostramos el contenido recibido
            Console.WriteLine($"Error al deserializar JSON: {ex.Message}");
            Console.WriteLine($"Contenido recibido: {rawResponse}");
            throw;
        }
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