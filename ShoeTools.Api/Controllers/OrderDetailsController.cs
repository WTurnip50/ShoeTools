using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailsRepository _orderDetailsRepository;
   
    public  OrderDetailsController(IOrderDetailsRepository orderDetailsRepository)
    {
        _orderDetailsRepository = orderDetailsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<OrderDetails>>>> GetAllOrdersAsync()
    {
        var ordersDetails = await _orderDetailsRepository.GetOrders();
        var response = new Response<List<OrderDetails>>();
        response.Data = ordersDetails;
        return Ok(response);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<OrderDetails>>> GetById(int id)
    {
        var order = await _orderDetailsRepository.GetOrderItemById(id);
        var response = new Response<OrderDetails>();
        response.Data = order;
        if (order == null)
        {
            response.Message = "Item not found";
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<OrderDetails>>> Post([FromBody] OrderDetails orders)
    {
        orders = await _orderDetailsRepository.SaveAsync(orders);
        var response = new Response<OrderDetails>();
        response.Data = orders;
        
        return Created($"/api/[controller]/{orders.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<OrderDetails>>> Update([FromBody] OrderDetails orders)
    {
        var result = await _orderDetailsRepository.UpdateAsync(orders);
        var response = new Response<OrderDetails> { Data = result };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _orderDetailsRepository.DeleteAsync(id);
        response.Data = result;
        if (result == null)
        {
            response.Message = "Item not found or is not deleted";
            return NotFound(response);
        }
        return Ok(response);
    }
}