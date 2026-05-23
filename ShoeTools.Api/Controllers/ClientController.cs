using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
   
    public  ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<ClientDto>>>> GetAllAsync()
    {
        var response = new Response<List<ClientDto>>()
        {
            Data = await  _clientService.GetAllClients()
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ClientDto>>> GetById(int id)
    {
        var response = new Response<ClientDto>();
        if (!await _clientService.ClientExists(id))
        {
            response.Errors.Add("Client not found");
            return NotFound(response);
        }

        response.Data = await _clientService.GetById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ClientDto>>> Post([FromBody] ClientDto client)
    {

        var response = new Response<ClientDto>
        {
            Data = await _clientService.SaveAsync(client)
        };
        
        return Created($"/api/[controller]/{client.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ClientDto>>> Update([FromBody] ClientDto client)
    {
        var response = new Response<ClientDto>();
        if (!await _clientService.ClientExists(client.Id))
        {
            response.Errors.Add("Client not found");
            return NotFound(response);
        }
        response.Data = await _clientService.UpdateAsync(client);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
       var result = await _clientService.DeleteAsync(id);
       response.Data = result;
        return Ok(response);
    }
}