using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailsService _orderDetailsService;
   
    public  OrderDetailsController(IOrderDetailsService orderDetailsService)
    {
        _orderDetailsService = orderDetailsService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<OrderDetailsDto>>>> GetAllOrdersAsync()
    {
        var response = new Response<List<OrderDetailsDto>>()
        {
            Data = await _orderDetailsService.GetAllOrders()
        };
        return Ok(response);
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<OrderDetailsDto>>> GetById(int id)
    {
        var response = new Response<OrderDetailsDto>();
        if (!await _orderDetailsService.OrderExists(id))
        {
            response.Errors.Add("Order not found");
            return NotFound(response);
        }
        response.Data = await _orderDetailsService.GetById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<OrderDetailsDto>>> Post([FromBody] OrderDetailsDto orders)
    {
        var response = new Response<OrderDetailsDto>()
        {
            Data = await _orderDetailsService.SaveAsync(orders)
        };
        return Created($"/api/[controller]/{orders.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<OrderDetailsDto>>> Update([FromBody] OrderDetailsDto orders)
    {
        var response = new Response<OrderDetailsDto>();
        if (!await _orderDetailsService.OrderExists(orders.Id))
        {
            response.Errors.Add("Order not found");
            return NotFound(response);
        }
        response.Data = await _orderDetailsService.UpdateAsync(orders);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _orderDetailsService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
}