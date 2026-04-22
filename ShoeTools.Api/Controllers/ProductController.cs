using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
   
    public  ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<Product>>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var response = new Response<List<Product>>();
        response.Data = products;
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<Product>>> GetById(int id)
    {
        var product = await _productRepository.GetProductById(id);
        var response = new Response<Product>();
        response.Data = product;
        if (product == null)
        {
            response.Message = "Product not found";
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Product>>> Post([FromBody] Product product)
    {
        product = await _productRepository.SaveAsync(product);
        var response = new Response<Product>();
        response.Data = product;
        
        return Created($"/api/[controller]/{product.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Product>>> Update([FromBody] Product product)
    {
        var result = await _productRepository.UpdateAsync(product);
        var response = new Response<Product> { Data = result };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _productRepository.DeleteAsync(id);
        response.Data = result;
        if (result == null)
        {
            response.Message = "Product not found";
            return NotFound(response);
        }
        return Ok(response);
    }
}