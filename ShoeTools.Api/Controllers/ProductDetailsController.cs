using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDetailsController : ControllerBase
{
    private readonly IProductDetailsRepository _productDetailsRepository;
   
    public  ProductDetailsController(IProductDetailsRepository productDetailsRepository)
    {
        _productDetailsRepository = productDetailsRepository;
    }
    
    [HttpGet]
    public async Task <ActionResult<Response<List<ProductDetails>>>> GetAllAsync()
    {
        var products = await _productDetailsRepository.GetAllAsync();
        var response = new Response<List<ProductDetails>>();
        response.Data = products;
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<ProductDetails>>> GetById(int id)
    {
        var product = await _productDetailsRepository.GetProductById(id);
        var response = new Response<ProductDetails>();
        response.Data = product;
        if (product == null)
        {
            response.Message = "Product not found";
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductDetails>>> Post([FromBody] ProductDetails product)
    {
        product = await _productDetailsRepository.SaveAsync(product);
        var response = new Response<ProductDetails>();
        response.Data = product;
        
        return Created($"/api/[controller]/{product.Id}",response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Product>>> Update([FromBody] ProductDetails product)
    {
        var result = await _productDetailsRepository.UpdateAsync(product);
        var response = new Response<ProductDetails> { Data = result };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _productDetailsRepository.DeleteAsync(id);
        response.Data = result;
        if (result == null)
        {
            response.Message = "Product not found";
            return NotFound(response);
        }
        return Ok(response);
    }
}