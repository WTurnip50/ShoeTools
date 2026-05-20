using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class ProductDto : DtoBase
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ProductDto()
    {
    }

    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
    }
}