using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
   
    public  ClientController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<Client>>>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        var response = new Response<List<Client>>();
        response.Data = clients;
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<Client>>> GetById(int id)
    {
        var client = await _clientRepository.GetClientById(id);
        var response = new Response<Client>();
        response.Data = client;
        if (client == null)
        {
            response.Message = "Client not found";
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Client>>> Post([FromBody] Client client)
    {
        client = await _clientRepository.SaveAsync(client);
        var response = new Response<Client>();
        response.Data = client;
        
        return Created($"/api/[controller]/{client.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Client>>> Update([FromBody] Client client)
    {
        var result = await _clientRepository.UpdateAsync(client);
        var response = new Response<Client> { Data = result };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _clientRepository.DeleteAsync(id);
        response.Data = result;
        if (result == null)
        {
            response.Message = "Client not found";
            return NotFound(response);
        }
        return Ok(response);
    }
}