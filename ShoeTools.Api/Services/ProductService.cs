using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> ProductExists(int productId)
    {
        var product = await _productRepository.GetProductById(productId);
        return product != null;
    }

    public async Task<ProductDto> SaveAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description =  productDto.Description,
            CreatedBy = "",
            CreatedDate =  DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        product = await _productRepository.SaveAsync(product);
        product.Id = product.Id;
        return  productDto;
    }

    public async Task<ProductDto> UpdateAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetProductById(productDto.Id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.UpdatedBy = "";
        product.UpdatedDate = DateTime.Now;
        
        await _productRepository.UpdateAsync(product);
        return productDto;
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        var productsDto = products.Select(product=>new ProductDto(product)).ToList();
        return productsDto;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        return await _productRepository.DeleteAsync(productId);
    }

    public async Task<ProductDto> GetById(int productId)
    {
        var product = await _productRepository.GetProductById(productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var productDto = new ProductDto(product);
        return productDto;
    }
}