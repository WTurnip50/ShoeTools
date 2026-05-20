using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
   
    public  OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<OrdersDto>>>> GetAllAsync()
    {
        var response = new Response<List<OrdersDto>>()
        {
            Data = await _orderService.GetAllOrders()
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<Orders>>> GetById(int id)
    {
        
        var response = new Response<OrdersDto>();
        if (!await _orderService.OrderExists(id))
        {
            response.Errors.Add("Order not found");
            return NotFound(response);
        }
        response.Data = await _orderService.GetById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<OrdersDto>>> Post([FromBody] OrdersDto dto)
    {

        var response = new Response<OrdersDto>
        {
            Data = await _orderService.SaveAsync(dto)
        };
        
        return Created($"/api/[controller]/{dto.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<OrdersDto>>> Update([FromBody] OrdersDto dto)
    {
        var response = new Response<OrdersDto>();
        if (!await _orderService.OrderExists(dto.Id))
        {
            response.Errors.Add("Order not found");
            return NotFound(response);
        }
        response.Data = await _orderService.UpdateAsync(dto);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _orderService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
}