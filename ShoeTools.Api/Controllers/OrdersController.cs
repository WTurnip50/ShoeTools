using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;
   
    public  OrdersController(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<Orders>>>> GetAllAsync()
    {
        var orders = await _ordersRepository.GetAllAsync();
        var response = new Response<List<Orders>>();
        response.Data = orders;
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<Orders>>> GetById(int id)
    {
        var order = await _ordersRepository.GetOrderById(id);
        var response = new Response<Orders>();
        response.Data = order;
        if (order == null)
        {
            response.Message = "Order not found";
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Orders>>> Post([FromBody] Orders orders)
    {
        orders = await _ordersRepository.SaveAsync(orders);
        var response = new Response<Orders>();
        response.Data = orders;
        
        return Created($"/api/[controller]/{orders.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Orders>>> Update([FromBody] Orders orders)
    {
        var result = await _ordersRepository.UpdateAsync(orders);
        var response = new Response<Orders> { Data = result };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _ordersRepository.DeleteAsync(id);
        response.Data = result;
        if (result == null)
        {
            response.Message = "Order not found";
            return NotFound(response);
        }
        return Ok(response);
    }
}