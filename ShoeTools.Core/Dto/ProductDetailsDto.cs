using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class ProductDetailsDto : DtoBase
{
    public int IdProduct { get; set; }
    public string Model { get; set; }
    public int Size { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public ProductDetailsDto()
    {
    }

    public ProductDetailsDto(ProductDetails productDetails)
    {
        Id = productDetails.Id;
        IdProduct = productDetails.IdProduct;
        Model = productDetails.Model;
        Size = productDetails.Size;
        Price = productDetails.Price;
        Stock = productDetails.Stock;
    }
}