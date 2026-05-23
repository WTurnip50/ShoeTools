using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services;

public class ProductDetailsService : IProductDetailsService
{
    private readonly IProductDetailsRepository _productDetailsRepository;

    public ProductDetailsService(IProductDetailsRepository productDetailsRepository)
    {
        _productDetailsRepository = productDetailsRepository;
    }

    public async Task<bool> ProductExists(int detailsId)
    {
        var details = await _productDetailsRepository.GetProductById(detailsId);
        return details != null;
    }

    public async Task<ProductDetailsDto> SaveAsync(ProductDetailsDto detailsDto)
    {
        var details = new ProductDetails
        {
            IdProduct =  detailsDto.IdProduct,
            Model =  detailsDto.Model,
            Price = detailsDto.Price,
            Size =  detailsDto.Size,
            Stock =  detailsDto.Stock,
            CreatedBy = "",
            CreatedDate =  DateTime.Now,
        };
        details = await _productDetailsRepository.SaveAsync(details);
        detailsDto.Id = details.Id;
        return detailsDto;
    }

    public async Task<ProductDetailsDto> UpdateAsync(ProductDetailsDto details)
    {
       var dto = await _productDetailsRepository.GetProductById(details.Id);
       if (dto == null)
       {
           throw new Exception("Product details not found");
       }
       dto.IdProduct = details.IdProduct;
       dto.Model = details.Model;
       dto.Price = details.Price;
       dto.Size = details.Size;
       dto.Stock = details.Stock;
       
       await _productDetailsRepository.UpdateAsync(dto);
       return new ProductDetailsDto(dto);
    }

    public async Task<List<ProductDetailsDto>> GetAllProductDetails()
    {
        var details = await _productDetailsRepository.GetAllAsync();
        var detailsDto =  details.Select(det => new ProductDetailsDto(det)).ToList();
        return detailsDto;
    }

    public async Task<bool> DeleteAsync(int detailsId)
    {
        return await _productDetailsRepository.DeleteAsync(detailsId);
    }

    public async Task<ProductDetailsDto> GetById(int detailsId)
    {
        var details = await _productDetailsRepository.GetProductById(detailsId);
        if (details == null)
        {
            throw new Exception("Details not found");
        }
        var  detailsDto = new ProductDetailsDto(details);
        return detailsDto;
    }
}