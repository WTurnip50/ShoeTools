using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDetailsController : ControllerBase
{
    private readonly IProductDetailsService _productDetailsService;
   
    public  ProductDetailsController(IProductDetailsService productDetailsService)
    {
        _productDetailsService = productDetailsService;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<ProductDetailsDto>>>> GetAllAsync()
    {
        var response = new Response<List<ProductDetailsDto>>()
        {
            Data = await _productDetailsService.GetAllProductDetails()
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ProductDetailsDto>>> GetById(int id)
    {
        var response = new Response<ProductDetailsDto>();
        if (!await _productDetailsService.ProductExists(id))
        {
            response.Errors.Add("Product details not found");
            return NotFound(response);
        }
        response.Data = await _productDetailsService.GetById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductDetails>>> Post([FromBody] ProductDetailsDto product)
    {
        var response = new Response<ProductDetailsDto>
        {
            Data = await _productDetailsService.SaveAsync(product)
        };
        return Created($"/api/[controller]/{response.Data.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProductDetailsDto>>> Update([FromBody] ProductDetailsDto product)
    {
        var response = new Response<ProductDetailsDto>();
        if (!await _productDetailsService.ProductExists(product.Id))
        {
            response.Errors.Add("Product details not found");
            return NotFound(response);
        }

        response.Data = await _productDetailsService.UpdateAsync(product);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _productDetailsService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
}