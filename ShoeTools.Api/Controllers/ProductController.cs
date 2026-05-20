using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
   
    public  ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<ProductDto>>>> GetAllAsync()
    {
        var response = new Response<List<ProductDto>>()
        {
            Data = await _productService.GetAllProducts()
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ProductDto>>> GetById(int id)
    {
        var response = new Response<ProductDto>();
        if (!await _productService.ProductExists(id))
        {
            response.Errors.Add("Product not found");
            return NotFound(response);
        }

        response.Data = await _productService.GetById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductDto>>> Post([FromBody] ProductDto productDto)
    {
        var response = new Response<ProductDto>
        {
            Data = await _productService.SaveAsync(productDto)
        };
        return Created($"/api/[controller]/{productDto.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProductDto>>> Update([FromBody] ProductDto productDto)
    {
        var response = new Response<ProductDto>();
        if (!await _productService.ProductExists(productDto.Id))
        {
            response.Errors.Add("Product not found");
            return NotFound(response);
        }

        response.Data = await _productService.UpdateAsync(productDto);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _productService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
}